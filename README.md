# Chat_Challange

<h3>Informações sobre o projeto</h3>
O projeto foi criado com base no  
<a href=https://github.com/LuizGPG/Chat_Challange/files/11158956/Chat_Challange.pdf>desafio</a>. 
O foco foi atender os pontos solicitados com clareza no codigo e também facilitando a implementção de novas funcionalidades ou correções futuras.<br/><br/>

<strong>Bonus feitos:</strong><br/>
· Use identidade .NET para autenticação de usuários - :white_check_mark:<br/>
· Lidar com mensagens que não são compreendidas ou quaisquer exceções levantadas dentro do bot.:white_check_mark:<br/>

<h2>Como executar o projeto</h2>

Para esse projeto utilizei a base de dados SQLServer pois alem da utilização tambem pode ser escalada facilmente.
Primeiramente deve ser necessário a criação da base de dados com o nome <strong>"Chat_Challange"</strong>.<br/>
![image](https://user-images.githubusercontent.com/14313148/230082264-60691a49-bffd-40f0-94b5-1f62603c6849.png)<br/>

Após isso será necessário atualizar a base. Executar os scripts:<br/>

<strong>update-database -Context ApplicationContext</strong><br/>
<strong>update-database -Context IdentityContext</strong><br/>

Agora será necessário instanciar um container para o RabbitMQ. <strong>Obs</strong>: Necessário ter o Docker instalado.<br/><br/>
<strong>docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management</strong><br/>
