# Manual
- [Manual do jogo](#manual-do-jogo)
- [Manual para clonagem do projeto](#manual-para-clonagem-do-projeto)
---
## Manual do jogo
---
## Manual para clonagem do projeto

### Unity:

Clone o repositorio e abra o projeto da pasta [\unity_project_meu_laranjo](..\unity_project_meu_laranjo) com o Unity Editor (preferencia: versão [2018.3.0](https://unity3d.com/pt/get-unity/download/archive))

### Servidor:

Hospede os arquivos da pasta [\host_server](..\host_server) em um servidor HTTP com PHP 5.6 e uma base de dados

### Bases de Dados:
Crie duas bases de dados SQL com os nomes "jovdev" e "meularanjo" no mesmo servidor do HTTP e execute, respectivamente, em cada uma as seguintes consultas para criar as tabelas

#### jovdev:
~~~~sql
CREATE DATABASE jovdev;

USE jovdev;

CREATE TABLE `usuario` (
    `id` bigint(20) NOT NULL,
    `confirm` varchar(7) COLLATE utf8_unicode_ci NOT NULL,
    `nome` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
    `sobrenome` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
    `nick` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
    `email` text COLLATE utf8_unicode_ci NOT NULL,
    `senha` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
    `criacao` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `nascimento` datetime NOT NULL,
    `lingua` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
    `id_gg` bigint(20) DEFAULT NULL,
    `id_fb` bigint(20) DEFAULT NULL,
    `id_tt` bigint(20) DEFAULT NULL,
    `id_nc` text COLLATE utf8_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;

ALTER TABLE `usuario`
    ADD PRIMARY KEY (`id`);

ALTER TABLE `usuario`
    MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=100000000000000;COMMIT;


CREATE TABLE `amizade` (
    `id` bigint(20) NOT NULL,
    `id_usuario_1` bigint(20) NOT NULL,
    `id_usuario_2` bigint(20) NOT NULL,
    `relacao` varchar(1) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE `amizade`
    ADD PRIMARY KEY (`id`);

ALTER TABLE `amizade`
    MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3,
    ADD FOREIGN KEY (`id_usuario_1`) REFERENCES `usuario`(`id`),
    ADD FOREIGN KEY (`id_usuario_2`) REFERENCES `usuario`(`id`);
~~~~

#### meularanjo:

~~~~sql
CREATE DATABASE meularanjo;

USE meularanjo;

CREATE TABLE `info_laranjo` (
    `id` bigint(20) NOT NULL,
    `nick_laranjo` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
    `moedas` bigint(20) NOT NULL,
    `dolares` bigint(20) NOT NULL,
    `nivel` float NOT NULL,
    `id_casa` int(11) NOT NULL,
    `quant_gar` int(11) NOT NULL,
    `relacionamento` varchar(1) COLLATE utf8_unicode_ci NOT NULL,
    `primeiro_cont` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ultimo_cont` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE `info_laranjo`
    ADD PRIMARY KEY (`id`);


CREATE TABLE `dados_info_laranjo` (
    `id` bigint(11) NOT NULL,
    `id_info_laranjo` bigint(20) NOT NULL,
    `indice` int(11) NOT NULL,
    `int_info` int(11) DEFAULT NULL,
    `txt_info` text COLLATE utf8_unicode_ci,
    `float_info` float DEFAULT NULL,
    `data_info` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE `dados_info_laranjo`
    ADD PRIMARY KEY (`id`),
    ADD FOREIGN KEY (`id_info_laranjo`) REFERENCES `info_laranjo`(`id`),
    CHANGE `id` `id` BIGINT(11) NOT NULL AUTO_INCREMENT;


CREATE TABLE `info` ( `ultima_versao` BIGINT NOT NULL ) ENGINE = MyISAM;
    INSERT INTO `info` (`ultima_versao`) VALUES ('9');
~~~~

### Dados de Exemplo:

#### jovdev

~~~~sql
~~~~

#### meularanjo

~~~~sql
~~~~

---