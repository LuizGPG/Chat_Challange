# Chat_Challange

<h3>Informações sobre o projeto</h3>
O projeto foi criado com base no  
<a href=https://github.com/LuizGPG/Chat_Challange/files/11158956/Chat_Challange.pdf>desafio</a>. 
O foco foi atender os pontos solicitados com clareza no codigo e também facilitando a implementção de novas funcionalidades ou correções futuras.<br/><br/>

<strong>Bonus feitos:</strong><br/>
· Use identidade .NET para autenticação de usuários - :white_check_mark:<br/>
· Lidar com mensagens que não são compreendidas ou quaisquer exceções levantadas dentro do bot.:white_check_mark:<br/>
· Crie um instalador. <i>Nesse ponto eu criei um executavel e ele esta no zip na raiz do projeto.</i>

<h3>Code coverage</h3>

![image](https://user-images.githubusercontent.com/14313148/230135091-31bdcf8b-cbac-40cb-bbf6-d04cf1541f33.png)

<h2>Como executar o projeto</h2>

Para esse projeto utilizei a base de dados SQLServer pois alem da utilização tambem pode ser escalada facilmente.
Primeiramente deve ser necessário a criação da base de dados com o nome <strong>"Chat_Challange"</strong>.<br/>
![image](https://user-images.githubusercontent.com/14313148/230082264-60691a49-bffd-40f0-94b5-1f62603c6849.png)<br/>

Após isso será necessário atualizar a base. Executar os scripts:<br/>
Eu executo dentro do VisualStudio no Package Manager.

<strong>update-database -Context ApplicationContext</strong><br/>
<strong>update-database -Context IdentityContext</strong><br/>

Agora será necessário instanciar um container para o RabbitMQ. <strong>Obs</strong>: Necessário ter o Docker instalado.<br/><br/>
<strong>docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management</strong><br/>

Com o projeto executando você caira em uma tela de login, caso nao possua conta, necessario registrar uma nova.<br/>

![image](https://user-images.githubusercontent.com/14313148/230154565-63e09533-a488-4ecd-8d93-8549a25835c9.png)

Registre sua conta e após isso faça login.

<h2> Ponderações finais </h2>

Quero agradecer a oportunidade de realizar o teste e o carisma que tiveram na entrevista.
Espero ter feito um projeto bom suficiente para embarcar nessa jornada com vocês para aprender muito e evoluir profissionalmente.
Também gostaria muito de um feedback independe de qual seja.

Obrigado
