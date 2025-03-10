CREATE TABLE saidas_estoque (
    id_saida INT AUTO_INCREMENT PRIMARY KEY,
    id_produto INT,
    quantidade INT NOT NULL,
    id_evento INT,
    data_saida TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_produto) REFERENCES produtos(id_produto),
    FOREIGN KEY (id_evento) REFERENCES eventos(id_evento)
);