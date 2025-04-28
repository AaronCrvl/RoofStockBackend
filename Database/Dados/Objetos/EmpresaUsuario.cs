namespace RoofStockBackend.Database.Dados.Objetos
{
    public class EmpresaUsuario
    {
        #region Propriedades Privadas
        int pID_EMPRESA { get; set; }
        int pID_USUARIO { get; set; }
        bool pIN_ATIVO { get; set; }
        #endregion

        #region Propriedades
        public int ID_EMPRESA
        {
            get
            {
                return this.pID_EMPRESA;
            }
            set
            {
                this.pID_EMPRESA = value;
            }
        }

        public int ID_USUARIO
        {
            get
            {
                return this.pID_USUARIO;
            }
            set
            {
                this.pID_USUARIO = value;
            }
        }

        public bool IN_ATIVO
        {
            get
            {
                return this.pIN_ATIVO;
            }
            set
            {
                this.pIN_ATIVO = value;
            }
        }
        #endregion

        #region Construtor
        public EmpresaUsuario()
        {
            this.ID_EMPRESA = -1;
            this.ID_USUARIO = -1;
            this.IN_ATIVO = false;
        }
        #endregion        
    }
}
