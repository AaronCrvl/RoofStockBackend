-- Criar EmpresaUsuario
CREATE TABLE EmpresaUsuario (
	ID_EMPRESA INT NOT NULL,
	ID_ESTOQUE INT NOT NULL,
	IN_ATIVO BIT NOT NULL DEFAULT(0),
	FOREIGN KEY (ID_USUARIO) REFERENCES Usuario(ID_USUARIO),
	FOREIGN KEY (ID_EMPRESA) REFERENCES Empresa(ID_EMPRESA)
);

-- Campo IN_ADMIN
ALTER TABLE Usuario ADD IN_ATIVO BIT NOT NULL DEFAULT 0;