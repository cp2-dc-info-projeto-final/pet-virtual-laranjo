# Especificação de casos de uso


## Sumário

- [CDU 01 - Registro](#cdu-01---cadastro)
- [CDU 02 - Login](#cdu-02---login)
- [CDU 03 - Recuperar Senha](#CDU-03---Recuperar-Senha)
- [CDU 04 - Atualizar Dados](#CDU-04---Atualizar-Dados)
- [CDU 05 - Salvar Dados](#CDU-05---Salvar-Dados)
- [CDU 06 - Jogar Mini-Game](#CDU-06---Jogar-Mini-Game)
- [CDU 07 - Adicionar Amigos](#CDU-07---Adicionar-Amigos)
- [CDU 08 - Comprar Item](#CDU-08---Comprar-Item)
- [CDU 09 - Equipar Item](#CDU-09---Equipar-Item)
- [CDU 10 - Comprar Moedas](#CDU-10---Comprar-Moedas)
- [CDU 11 - Comprar "Dolares"](#CDU-11---Comprar-"Dolares")



## CDU 01 - Registro

**Atores:** Usuários

**Pré-condições:** O usuário dever possuir e-mail

**Fluxo principal:**

1. O usuário deverá fornecer: um apelido (nick) que não foi utilizado para outro registro, primeiro nome, sobrenome, endereço de e-mail que não foi utilizado para outro registro, senha, confirmação de senha, data de nascimento e selecionar um idioma dentro de uma lista
2. O usuário deverá confirmar seu endereço de e-mail acessando um link que será enviado para ele

**Fluxo Alternativo 1**
1. O usuário deixa algum campo vazio
2. O botão de prosseguir com o cadastro fica desabilitado

**Fluxo Alternativo 2**
1. O usuário fornece um apelido (nick) e/ou um endereço de e-mail que já foi utilizado em outro cadastro
2. O usuário recebe um aviso informando os dados estão indisponíveis

## CDU 02 - Login

**Atores:**  Usuários

**Pré-condições:** O usuário dever possuir um Registro e não pode estar logado com outra conta no dispositivo

**Fluxo principal:**

1. O usuário deverá executar o login informando e-mail ou apelido (nick) e senha.
2. Caso o login seja validado e não possuir uma sessão salva on-line, ela será criada a partir da sessão off-line atual.

**Fluxo Alternativo 1**

1. O usuário deverá executar o login informando e-mail ou apelido (nick) e senha.
2. Caso o login seja validado e possuir uma sessão salva on-line, o usuário deverá escolher se sobrescreve a sessão off-line com a on-line ou o contrario

**Fluxo Alternativo 2**

1. O usuário deverá executar o login informando e-mail ou apelido (nick) e senha.
2. Caso o login seja invalidado o usuário será orientado se a senha não é válida (caso o e-mail/nick esteja cadastrados) e recomendará recuperar a senha ou se o e-mail/login não estão cadastrados

## CDU 03 - Recuperar Senha

**Atores:** Usuários

**Pré-condições:** O usuário dever possuir um Registro, não pode estar logado com alguma conta no dispositivo e estar na tela de login

**Fluxo principal:**
1. O usuário seleciona o botão de recuperar contar
2. Insere seu e-mail ou apelido (nick) correspondente a contar
3. Seleciona o botão de enviar
4. Um e-mail será enviado ao endereço correspondente com a senha do usuário e um link para ele trocar a senha em um site externo

## CDU 04 - Atualizar Dados

**Atores:** Usuários

**Pré-condições:** Haver uma requisição dos dados serem atualizados

**Fluxo principal:**

1. Quando a atualização de dados for requisitada, se o usuário estiver com conexão a internet e estiver logado, e a última sessão salva off-line tiver a mesma data e hora da salva on-line, a atual será sobrescritas com a on-line

**Fluxo Alternativo 1**

1. Quando a atualização de dados for requisitada, se o usuário estiver com conexão à internet e estiver logado, e a última sessão salva off-line tiver a data e hora posteriores da salva on-line, a atual e a on-line serão sobrescritas com a off-line

**Fluxo Alternativo 2**

1. Quando a atualização de dados for requisitada, se o usuário estiver com conexão à internet e estiver logado, e a última sessão salva off-line tiver a data e hora anteriores da salva on-line, a atual e a off-line serão sobrescritas com a on-line

**Fluxo Alternativo 3**

1. Quando a atualização de dados for requisitada, se o usuário estiver sem conexão a internet e estiver logado, a sessão atual será sobrescrita com a off-line.

**Fluxo Alternativo 4**

1. Quando a atualização de dados for requisitada, se o usuário estiver deslogado, a sessão atual será sobrescrita com a off-line, caso exista (do contrário, será criada uma nova)

## CDU 05 - Salvar Dados

**Atores:** Usuários

**Pré-condições:** Haver uma requisição dos dados serem salvos

**Fluxo Principal:**

1. Quando o salvamento de dados for requisitado, caso o usuário esteja logado e com internet, sua sessão off-line e on-line será sobrescritas com a atual

**Fluxo Alternativo:**

1. Quando o salvamento de dados for requisitado, caso o usuário esteja deslogado e/ou sem acesso à internet, sua sessão off-line será sobrescrita com a atual





## CDU 06 - Jogar Mini-Game

**Atores:** Usuários

**Pré-condições:** -  -  -

**Fluxo principal:**

1. O usuário escolhe um minigame da lista de minigames e a partida correspondente se inicia
2. Ao fim da partida, sua pontuação em moedas é acrescida ás moedas que ele possui, caso um recorde seja quebrado, a nova marca deve ser atualizada e o salvamento será requisitado

# CDU 07 - Adicionar Amigos

**Atores:** Usuários

**Pré-condições:** Estar cadastrado e logado, e estar na tela de adicionar amizades.

**Fluxo principal:**

1. Um usuário deverá inserir o apelido (nick) do usuário que ele quer solicitar amizade
2. O usuário solicitado aceita a solicitação em seguida, e então cada um será adicionado à lista de amigos do outro

**Fluxo Alternativo**

1. O Usuário solicitado a solicitação em seguida e nada acontece.

# CDU 08 - Comprar Item

**Atores:** Usuários

**Pré-condições:** O usuário estar na tela de compra do respectivo item.

**Fluxo principal:**
1. O usuário seleciona o item que será equipado ao modelo de pré-visualização
2. Seleciona o botão de comprar
3. Caso o jogador tenha a quantidade de moedas ou "dolares" correspondente, essa quantidade será subtraída, o item será adicionado a respectiva lista de aquisição e o salvamento de dados será requisitado

**Fluxo Alternativo:**
Caso o jogador não tenha a quantidade de moedas ou "dolares" correspondente, ele será notificado disso junto a um botão de comprar mais moedas ou "dolares"

# CDU 09 - Equipar Item

**Atores:** Usuários

**Pré-condições:** O usuário estar na tela da lista do respectivo item e possuir o item.

**Fluxo principal:**
1. O usuário seleciona o item que será equipado ao modelo de pré-visualização
2. Seleciona o botão de equipar
3. O item entrará para a respectiva lista de itens equipados e o salvamento de dados será requisitado

# CDU 10 - Comprar Moedas

**Atores:** Usuários

**Pré-condições:** O usuário estar na tela de compra de moedas

**Fluxo principal:**

1.  O usuário seleciona a quantidade de moedas da lista
2. Caso o usuário tenha uma quantidades de "Dolares" correspondente, ele será notificado a confirmar se deseja realmente comprar as moedas
3. Caso confirme, o as moedas são acrescidas e os dolares, subtraídos e o salvamento de dados será requisitado

**Fluxo Alternativo 1**
1.  O usuário seleciona a quantidade de moedas da lista
2. Caso o usuário tenha uma quantidades de "Dolares" correspondente, ele será notificado a confirmar se deseja realmente comprar as moedas
3. Caso não confirme, nada acontece

**Fluxo Alternativo 2**
1.  O usuário seleciona a quantidade de moedas da lista
2. Caso o usuário não tenha uma quantidades de "Dolares" correspondente, ele será notificado que não pode estar apto a comprar as moedas e que ele pode comprar adquirir mais "Dolares" com um botão que abre a tela de compra de "Dolares"

# CDU 11 - Comprar "Dolares"

**Atores:** Usuários

**Pré-condições:** O usuário estar na tela de compra de "dolares"

**Fluxo principal:**

1.  O usuário seleciona a quantidade de "Dolares" da lista
2. Uma janela de pagamento da PlayStore/AppStore abrirá para dar início a transação do valor correspondente
3. Caso a transação seja efetuada com sucesso, a quantidade de "Dolares” e o salvamento de dados será requisitado, caso contrário, um aviso de falha será exibido
