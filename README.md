# OpenSocials

 ----Processo criação App desenvolvedor na Meta com acesso a API----
 https://developers.facebook.com/docs/development/register?locale=pt_BR
 https://developers.facebook.com/apps/1047149719835781/dashboard/

1. Entrar em uma conta desenvolvedor da meta caso possua uma, caso contrário, se registrar com a conta do Facebook
2. Escolher a opção de desenvolvedor
3. Após acessar a conta, clicar em "Criar primeiro app" se for o primeiro registro, ou em "Criar app" se algum já tiver sido criado
4. Escolher o caso de uso do app, baseado nas permissões e interação do app com os usuários
5. Inserir email e nome  atribuído ao app
6. Escolher o tipo do app como "business" para ter o acesso de conexão com a API
7. Obs: O usuário administrador e criador do App consegue rodar a aplicação sem efetivamente publicá-la, desde que esteja no modo desenvolvedor como administrador.

**Funcionalidades** 

- Registro (Register)
    - Aqui é possível criar uma conta no site, para poder ter acesso as funcionalidades disponibilizadas.
- Autenticação (Login)
    - Aqui é possível fazer a autenticação do usuário no site, na qual irá ser redirecionado para as telas de noticias e acesso as funcionalidades. Nesta tela também é possivel navegar para o registro caso não tenha conta.
- Noticias (News)
    - Está é a tela principal do site, aqui é possível visualizar todas as noticias postadas em sua conta, e fazer filtros de dias, status de aprovação caso for administrador.
    - Contém os cards das noticias, podendo visualizar as informações e editar uma noticia caso necessário.
    - Contém um botão de criação de uma nova noticia, assim podendo inserir noticias na sua conta Meta.
    - Contém acesso a todas as funcionalidades que usuário tiver permissão.
- Criação de Notícias (Create News)
    - Nesta tela é possível criar uma nova notícia, podendo inserir titulo, descrição, uma imagem, vídeo e fazer a criação.
- Edição de Notícias  (Edit News)
    - Nesta tela é possível fazer a edição de uma notícia, podendo editar título, descrição e medias.
- Configuração (Config)
    - Está é a tela de configuração da parte da Meta API do site, na qual o usuário podera conectar e inserir os dados de sua conta Meta. Podendo seguir o documento de configuração citado anteriormente.
- Logs (Logs)
    - Está é a tela de Logs do site, nela é possível visualizar todos os registros feitos pelos usuários. Apenas o administrador poderá ver qual alterações os usuários fizeram.
- Controle de Notícias (Verify News)
    - Está tela é possível fazer aprovações de noticias postadas por usuários. É somente possível fazer o controle da noticias caso for administrador.
- About (Sobre)
    - Esta tela contém informações sobre o projeto Bons Fluídos na qual é a base do site. Toda a parte de layout foi voltado para o Projeto, desde a utilização de sua logo e paleta de cores.
      
**Passos para rodar o projeto.**

1. Baixar o Visual Studio - [Baixar Ferramentas do Visual Studio – Instalação gratuita para Windows, Mac e Linux (microsoft.com)](https://visualstudio.microsoft.com/pt-br/downloads/)
2. Caso não tenha o Git, baixar o Git- [Git (git-scm.com)](https://git-scm.com/)
3. Fazer o clone do repositório:
    1. Comando git clone  https://github.com/kuramayouko/OpenSocials.git 
4. Abrir o projeto com o Visual Studio e selecionar (clicar duas vezes) o arquivo OpenSocials.sln
5. Iniciar o projeto clicando em cima do botão de dar Play no campo superior.
   ![image](https://github.com/kuramayouko/OpenSocials/assets/91897050/511b11ec-b649-4ea7-905c-5914f0139ddf)
   
7. Pronto ! Seu projeto está rodando.
