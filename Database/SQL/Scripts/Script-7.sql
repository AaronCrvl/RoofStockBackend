CREATE TABLE movimentacoes_estoque (
    id_movimentacao INT AUTO_INCREMENT PRIMARY KEY,
    tipo_movimentacao ENUM('entrada', 'saida') NOT NULL,
    id_produto INT,
    quantidade INT NOT NULL,
    id_evento INT NULL,
    id_fornecedor INT NULL,
    data_movimentacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_produto) REFERENCES produtos(id_produto),
    FOREIGN KEY (id_evento) REFERENCES eventos(id_evento),
    FOREIGN KEY (id_fornecedor) REFERENCES fornecedores(id_fornecedor)
);