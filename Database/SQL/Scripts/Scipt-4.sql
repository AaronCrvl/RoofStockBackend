CREATE TABLE eventos (
    id_evento INT AUTO_INCREMENT PRIMARY KEY,
    nome_evento VARCHAR(255) NOT NULL,
    data_inicio DATE,
    data_fim DATE,
    descricao TEXT,
    localizacao VARCHAR(255)
);