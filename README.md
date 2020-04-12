# Meu Laranjo

![](\documentacao\readme\imagens\laranjo_img.png)

---

### Languages:
[![](\documentacao\readme\icones\band_br.png)](#sumário) [![](\documentacao\readme\icones\band_us.png)](#summary)

---

## Sumário:

- [Sobre o projeto](#sobre-o-projeto)
- [Origem](#origem)
- [Como utilizar](#como-utilizar)
    - [Manual do jogo](#manual-do-jogo)
    - [Manual para clonagem do projeto](#manual-para-clonagem-do-projeto)

---

## Sobre o projeto

Meu Laranjo é um jogo que transforma o meme do Laranjo em um pet virtual similar ao conhecido [Pou](https://play.google.com/store/apps/details?id=me.pou.app&hl=pt_BR).

O jogo consiste em jogar mini-games para conseguir moedas que serao utilizadas para comprar itens de customização do personagem, como cabelo e roupa, e do ambiente, como moveis da casa.

Seu Front-End é desenvolvido com a engine [Unity 3D](https://unity.com/) (C#) e seu Back-End usa PHP para acesso ao banco de dados e futuramente utilizará NodeJS para conexões multi-jogador.

---

## Origem

Meu Laranjo foi um projeto de jogo para ser usado como trabalho de conclusão de curso (TCC) no curso tecnico de Desenvolvimento de Sistemas do ensino médio do [Colégio Pedro II](http://www.cp2.g12.br/index.php) - [Campus Duque de Caxias](http://www.cp2.g12.br/blog/duquedecaxias/) no ano de 2019

---

# Como utilizar

- [Manual do jogo](#manual-do-jogo)
- [Manual para clonagem do projeto](#manual-para-clonagem-do-projeto)

---

## Manual do jogo

Ao abrir o jogo pela primeira vez, você deverá selecionar um idioma da lista (ele pode ser mudado posteriormente em ![](\documentacao\readme\icones\but_op.png) -> ![](\documentacao\readme\icones\but_li.png))

![](\documentacao\readme\screenshots\scr_pt_li.jpg)

Uma verificação de versão acontecerá para assegurar que seu jogo está atualizado.

![](\documentacao\readme\gifs\lar_atu.gif)

Depois disso você estará na parte central do jogo: a casa do Laranjo.

Toque no chão para fazer o Laranjo andar até lá.

![](\documentacao\readme\gifs\lar_cli.gif)

Toque nas setas para mudar a visão da camera entre: sala de estar, quarto, banheiro e cozinha

![](\documentacao\readme\screenshots\scr_pt_ca.jpg)

No quarto, toque em ![](\documentacao\readme\icones\but_ar.png) para abrir o guarda-roupa do Laranjo e toque em ![](\documentacao\readme\icones\but_ce.png) para acessar a lojas (![](\documentacao\readme\icones\but_in.png)), minigames (![](\documentacao\readme\icones\but_jo.png)) e amigos (![](\documentacao\readme\icones\but_am.png)).


<img src="documentacao\readme\gifs\lar_arm.gif" width="25%"><img src="documentacao\readme\gifs\lar_loj.gif" width="25%"><img src="documentacao\readme\gifs\lar_jog.gif" width="25%"><img src="documentacao\readme\gifs\lar_ami.gif" width="25%">


Para abrir o menu de opções toque no botao superior direito (![](\documentacao\readme\icones\but_op.png)), nele você pode pode acessar as áreas de perfil, idiomas, configuracoes e créditos

![](\documentacao\readme\screenshots\scr_pt_op.jpg)

---

## Manual para clonagem do projeto

Para abrir o projeto em modo de desenvolvimento, você precisa ter um ambiente configurado com Unity 3D (preferencia: versão [2018.3.14](https://unity3d.com/pt/get-unity/download/archive)) e um servidor HTTP com PHP e uma base de dados MySQL (recomendo: [EasyPHP DevServer](https://www.easyphp.org/))

### Unity:

Clone o repositorio e abra o projeto da pasta [\unity_project_meu_laranjo](\unity_project_meu_laranjo) com o Unity Editor 

### Servidor:

Hospede os arquivos da pasta [\host_server](\host_server) no servidor HTTP

### Bases de Dados:

Utilize essas consultas para gerar as bases de dados "jovdev" e "meularanjo" no mesmo servidor do HTTP.

(A estrutura dessas bases de dados estão descritas nesse [diagrama de entidades e relacionamentos](\documentacao\diagrama%20do%20banco%20de%20dados\DED%20bases%20de%20dados.jpg) )

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
    //em breve...
~~~~

#### meularanjo

~~~~sql
    //em breve...
~~~~

---

Visite o site da [JovDev](http://jovdev.tk/jogos.html) para futuras informações e o [cronograma](https://github.com/orgs/cp2-dc-info-projeto-final/projects/5) para acompanhar o desenvolvimento do projeto :)

---
## Summary:

- [About the project](#about-the-project)
- [Why?](#why)
- [Getting started](#getting-started)
    - [Manual do jogo](#manual-do-jogo)
    - [Manual para clonagem do projeto](#manual-para-clonagem-do-projeto)

---

## About the project

Meu Laranjo is a game that turns Laranjo's meme into a virtual pet similar to the well-known [Pou](https://play.google.com/store/apps/details?id=me.pou.app&hl=en_US).

The game consists of playing mini-games to get coins that will be used to buy items to customize the character, such as hair and clothes, and the environment, such as furniture in the house.

Its Front-End is developed with the engine [Unity 3D](https://unity.com/) (C #) and its Back-End uses PHP to access the database and in the future it will use NodeJS for multiplayer connections.

---

## Why?

Meu Laranjo was a game project to be used as a course completion work in the technical course on Systems Development of high school at [Colégio Pedro II](http://www.cp2.g12.br/index.php) - [Campus Duque de Caxias](http://www.cp2.g12.br/blog/duquedecaxias/) in 2019

---

## Getting Started

- [Game manual](#game-manual)
- [Manual for cloning the project](#manual-for-cloning-the-project)

---

## Game manual

When opening the game for the first time, you must select a language from the list (it can be changed later at ![](\documentacao\readme\icones\but_op.png) -> ![](\documentacao\readme\icones\but_li.png))

![](\documentacao\readme\screenshots\scr_pt_li.jpg)

A version check will take place to ensure that your game is up to date.

![](\documentacao\readme\gifs\lar_atu.gif)

After that you will be in the central part of the game: the Laranjo house.

Touch the floor to make Laranjo walk there.

![](\documentacao\readme\gifs\lar_cli.gif)

Touch the arrows to change the camera view between: living room, bedroom, bathroom and kitchen

![](\documentacao\readme\screenshots\scr_pt_ca.jpg)

In the bedroom, tap ![](\documentacao\readme\icones\but_ar.png) to open the Laranjo wardrobe and tap ![](\documentacao\readme\icones\but_ce.png) to access stores (![](\documentacao\readme\icones\but_in.png)), minigames (![](\documentacao\readme\icones\but_jo.png)) and friends (![](\documentacao\readme\icones\but_am.png)).


<img src="documentacao\readme\gifs\lar_arm.gif" width="25%"><img src="documentacao\readme\gifs\lar_loj.gif" width="25%"><img src="documentacao\readme\gifs\lar_jog.gif" width="25%"><img src="documentacao\readme\gifs\lar_ami.gif" width="25%">


To open the options menu, tap the upper right button (![](\documentacao\readme\icones\but_op.png)), where you can access the profile, languages, settings and credits areas

![](\documentacao\readme\screenshots\scr_pt_op.jpg)

---

## Manual for cloning the project

To open the project in development mode, you need to have an environment configured with Unity 3D (preferably: version [2018.3.14](https://unity3d.com/en/get-unity/download/archive)) and a server HTTP with PHP and a MySQL database (I recommend: [EasyPHP DevServer](https://www.easyphp.org/))

### Unity:

Clone the repository and open the project from the folder [\ unity_project_meu_laranjo](\unity_project_meu_laranjo) with Unity Editor

### Server:

Host files from the [\host_server](\host_server) folder on the HTTP server

### Data base:

Use these queries to generate the "jovdev" and "meularanjo" databases on the same HTTP server.

(The structure of these databases are described in this [diagram of entities and relationships](\documentacao\diagrama%20do%20banco%20de%20dados\DED%20bases%20de%20dados.jpg) )

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

### Sample Data:

#### jovdev

~~~~sql
    //coming soon...
~~~~

#### meularanjo

~~~~sql
    //coming soon...
~~~~

---

Visit the [JovDev](http://jovdev.tk/jogos.html) website  for future information and the [schedule](https://github.com/orgs/cp2-dc-info-projeto-final/projects/5) to follow the development of the project :)

---