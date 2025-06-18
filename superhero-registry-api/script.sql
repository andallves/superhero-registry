-- TABELAS

CREATE TABLE super_poder (
                             id SERIAL PRIMARY KEY,
                             nome VARCHAR(255) NOT NULL,
                             descricao TEXT
);

CREATE TABLE heroi (
                       id SERIAL PRIMARY KEY,
                       nome VARCHAR(255) NOT NULL,
                       nome_heroi VARCHAR(255) NOT NULL,
                       data_nascimento DATE,
                       altura FLOAT NOT NULL,
                       peso FLOAT NOT NULL,
                       desativado BOOLEAN DEFAULT FALSE,
                       criado_em TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       atualizado_em TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       deletado BOOLEAN DEFAULT FALSE
);

CREATE TABLE heroi_super_poder (
                                   id SERIAL PRIMARY KEY,
                                   heroi_id INTEGER NOT NULL REFERENCES heroi(id) ON DELETE CASCADE,
                                   super_poder_id INTEGER NOT NULL REFERENCES super_poder(id) ON DELETE CASCADE,
                                   UNIQUE(heroi_id, super_poder_id)
);

-- DADOS DE SUPERPODERES

INSERT INTO super_poder (nome, descricao) VALUES
                                              ('Super Força', 'Capacidade de levantar objetos extremamente pesados'),
                                              ('Voo', 'Capacidade de voar'),
                                              ('Invisibilidade', 'Capacidade de se tornar invisível aos olhos'),
                                              ('Super Velocidade', 'Capacidade de se mover em velocidades extraordinárias'),
                                              ('Telepatia', 'Capacidade de ler e influenciar pensamentos');

-- DADOS DE HERÓIS

INSERT INTO heroi (nome, nome_heroi, data_nascimento, altura, peso) VALUES
                                                                        ('Clark Kent', 'Superman', '1978-06-18', 1.90, 95.0),
                                                                        ('Diana Prince', 'Mulher-Maravilha', '1984-03-22', 1.80, 75.0),
                                                                        ('Barry Allen', 'Flash', '1990-09-01', 1.82, 80.0);

-- RELAÇÃO HERÓIS X SUPERPODERES

-- Superman: Super Força, Voo
INSERT INTO heroi_super_poder (heroi_id, super_poder_id) VALUES
                                                             (1, 1), -- Super Força
                                                             (1, 2); -- Voo

-- Mulher-Maravilha: Super Força, Telepatia
INSERT INTO heroi_super_poder (heroi_id, super_poder_id) VALUES
                                                             (2, 1), -- Super Força
                                                             (2, 5); -- Telepatia

-- Flash: Super Velocidade, Invisibilidade
INSERT INTO heroi_super_poder (heroi_id, super_poder_id) VALUES
                                                             (3, 4), -- Super Velocidade
                                                             (3, 3); -- Invisibilidade
