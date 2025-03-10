using RoofStockBackend.Contextos;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Usuario
    {
        #region Propriedades
        public long Id { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }

        #endregion

        #region Métodos Públicos
        public void AlterarUsuario()
        {
            try
            {
                _ = ctxUsuario.AlterUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CriarUsuario()
        {
            try
            {
                _ = ctxUsuario.CreateUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarUsuario()
        {
            try
            {
                _ = ctxUsuario.AlterUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion      
    }
}
