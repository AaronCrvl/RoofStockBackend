CREATE TABLE entradas_estoque (
    id_entrada INT AUTO_INCREMENT PRIMARY KEY,
    id_produto INT,
    quantidade INT NOT NULL,
    data_entrada TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    id_fornecedor INT,
    FOREIGN KEY (id_produto) REFERENCES produtos(id_produto),
    FOREIGN KEY (id_fornecedor) REFERENCES fornecedores(id_fornecedor)
);