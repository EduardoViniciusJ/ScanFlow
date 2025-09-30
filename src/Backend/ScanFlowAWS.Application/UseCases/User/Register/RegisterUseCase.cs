using AutoMapper;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    /// <summary>
    /// Caso de uso responsável pelo registro de novos usuários no sistema.
    /// Realiza validação dos dados, criptografa a senha e salva o usuário no banco de dados.
    /// </summary>
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor do caso de uso <see cref="RegisterUseCase"/>.
        /// </summary>
        /// <param name="mapper">Serviço de mapeamento entre DTO e entidade.</param>
        /// <param name="userRepository">Repositório para persistência de usuários.</param>
        /// <param name="passwordEncripter">Serviço para encriptação de senhas.</param>
        /// <param name="unitOfWork">Unit of Work para salvar no banco de dados.</param>
        public RegisterUseCase(
            IMapper mapper,
            IUserWriteOnlyRepository userRepository,
            IPasswordEncripter passwordEncripter,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordEncripter = passwordEncripter;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executa o registro de um novo usuário.
        /// </summary>
        /// <param name="request">Objeto contendo dados do usuário a ser registrado.</param>
        /// <returns>Objeto <see cref="ResponseRegisterUserJson"/> com os dados do usuário registrado.</returns>
        /// <exception cref="ErrorOnValidationException">Lançada quando a validação do usuário falha.</exception>
        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            ValidateRequest(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.PasswordHash = _passwordEncripter.Encrypt(request.Password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Username = user.Username,
                Email = user.Email,
            };
        }

        /// <summary>
        /// Valida os dados do usuário usando <see cref="RegisterUserValidator"/>.
        /// </summary>
        /// <param name="request">Requisição contendo os dados do usuário.</param>
        /// <exception cref="ErrorOnValidationException">Lançada se algum dado estiver inválido.</exception>
        private void ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}