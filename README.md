
![image](https://github.com/user-attachments/assets/af617c49-717d-4aef-92cf-7adb3d973728)


## Olá, time! Bem-vindos ao nosso projeto Open Source, KRATOS. 🪓


> [!WARNING]
> **O que é o projeto Kratos?**
>
> Um sistema poderoso desenvolvido utilizando .NET 9 e arquitetura limpa, seguindo as melhores práticas da indústria para garantir qualidade, escalabilidade e manutenção.
>
> A ideia inicial é auxiliar desenvolvedores com operações simples, como a criação de CRUDs, que podem consumir muito tempo no dia a dia. O foco deste projeto é justamente otimizar o tempo, permitindo que possamos dedicar mais atenção a lógicas mais complexas — algo que o Kratos não é capaz de lidar.

Independentemente da empresa ou do setor, o Kratos estará sempre pronto para auxiliá-lo no desenvolvimento com um desempenho extremamente elevado.

> [!IMPORTANT]
> Para aqueles que desejam acompanhar o desenvolvimento: https://github.com/users/thiagocorreanet/projects/31/views/1

### Estrutura do projeto

**WEB**
- **Controllers:** Chamadas para realizar operações na estrutura para nossas funcionalidades.
- **ViewComponents:** Componente para utilizar na reutilização de parte da view.
- **Views:** Páginas HTML (Front)
- **wwwroot - dist - js - custom.js:** Arquivo javascript para utilizar como funções padrões do projeto.
- **wwwroot - dist - js - pastas:** Dentro de cada pasta existe funções javascript para funçõs das telas.

**Application**
- **Commands:** Estrutura para usar comandos de aplicação, como criar, atualizar ou excluir.
- **Extension:** Pasta responsável por gerar código para cada cadama do projeto  (Gerador de fonte).
- **Notification:** Estrutura de aplicação para notificar a API de que algo deu errado e reportar isso à nossa interface gráfica.
- **Queries:** Estrutura para usar consultas de aplicação para recuperação de dados do banco de dados. Tanto os comandos quanto as consultas fazem parte do padrão de arquitetura CQRS.
- **Validators:** Estrutura para realizar validações utilizando Fluent Validations.

**Core**
- **Repositories:** Abstrações de repositórios.
- **Entities:** Classes que irão representar nossas entidades.
- **Enums:** Enumeradores do projeto.
 
**Infrastructure**
- Auth: Lógica para gerenciar o acesso de usuários utilizando claims.
- Migrations: Migração do EF da estrutura C# => Banco de dados
- Persistence:
  - Configuration: Estrutura de configuração para campos do banco de dados.
  - Repositories: Métodos para acesso a dados.
 
Espero que você esteja gostando! Venha contribuir com o nosso projeto. ❤️

  
