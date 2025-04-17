using Microsoft.IdentityModel.Tokens;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos;
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

        public async Task<string> AutenticarDadosToken(LoginUsuarioDto loginDto)
        {
            var usuario = await _usuarioRepository.GetAllAsync();
            usuario.FirstOrDefault(u => (u.TX_SENHA == loginDto.senha && u.TX_LOGIN == loginDto.login));
            if (usuario == null)
                return string.Empty;

            return GerarTokenJWT(loginDto);
        }

        private static string GerarTokenJWT(LoginUsuarioDto loginDto)
        {
            var claims = new[]
                        {
                new Claim(JwtRegisteredClaimNames.Sub, loginDto.login),
                new Claim(JwtRegisteredClaimNames.Jti, loginDto.senha)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_keyyour_super_secret_keyyour_super_secret_key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: "yourdomain.com",
                //audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}