using Microsoft.IdentityModel.Tokens;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Login;
using RoofStockBackend.Repositorios;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace RoofStockBackend.Serviços
{
    public class SrvcAutenticacao
    {
        private Repository<Usuario> _usuarioRepository;

        public SrvcAutenticacao(AppDbContext context)
        {
            _usuarioRepository = new Repository<Usuario>(context);
        }

        public async Task<string> AutenticarDadosToken(LoginDto loginDto)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuarioBusca = usuarios.FirstOrDefault(u => (u.TX_SENHA == loginDto.senha && u.TX_LOGIN == loginDto.login));
            if (usuarioBusca == null)
                return string.Empty;

            return GerarTokenJWT(loginDto);
        }

        private static string GerarTokenJWT(LoginDto loginDto)
        {
            var claims = new[]
                        {
                new Claim(JwtRegisteredClaimNames.Name, loginDto.login),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_keyyour_superyour_"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: "localhost",
                //audience: "audience1",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}