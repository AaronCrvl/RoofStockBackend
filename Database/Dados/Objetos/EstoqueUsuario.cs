using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class EstoqueUsuario
    {
        public EstoqueUsuario() {
            ID_USUARIO = -1;
            ID_USUARIO = -1;
            IN_ATIVO = false;
        }

        #region Propriedades Privadas
        int pID_USUARIO { get; set; }
        int pID_ESTOQUE { get; set; }
        bool pIN_ATIVO { get; set; }
        #endregion

        #region Propriedades
        [Required]
        [ForeignKey("Usuario")]
        public int ID_USUARIO
        {
            get {
                return pID_USUARIO;
            }
            set
            {
                pID_USUARIO = value;
            }
        }

        [Required]
        [ForeignKey("Estoque")]
        public int ID_ESTOQUE
        {
            get
            {
                return pID_ESTOQUE;
            }
            set
            {
                pID_ESTOQUE = value;
            }
        }

        public bool IN_ATIVO
        {
            get
            {
                return pIN_ATIVO;
            }
            set
            {
                pIN_ATIVO = value;
            }
        }
        #endregion
    }
}
