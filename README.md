![image](https://github.com/user-attachments/assets/9fde0aae-c661-4e7f-b404-cc10aadaf70b)

## Olá, sejam todos bem-vindos ao nosso projeto Open Source WebAPI KRATOS 🪓


> [!WARNING]
> *O que é o projeto Kratos?*
>
> Uma API poderosa desenvolvida com .NET 8 e arquitetura limpa, seguindo as melhores práticas do mercado para garantir qualidade, escalabilidade e manutenibilidade.
>
> A ideia inicial é auxiliar desenvolvedores com operações simples, como a criação de CRUDs, que podem consumir bastante tempo no dia a dia. O foco deste projeto é justamente otimizar o tempo, permitindo que possamos dedicar mais atenção a lógicas mais complexas algo que o Kratos não é capaz de fazer.

Seja qual for a empresa, qual for o segmento o Kratos sempre estará preparado para te auxliar no desenvolvimento com uma performace extramente alta.

### Estrutura do projeto

**API**
- Controllers: Endpoints do projeto
- SwaggerExtension: Classe responsável pela configuração personalizada do swagger.

**Application**
- Commands: Estrutura para utilizar os comandos da application seja create, update ou delete.
- Mapping: Classe responsável para fazer o mapeamento dos objetos.
- Notification: Estrutura da application para notificar a api que algo deu errado e reportar para nossa interface grafica.
- Queries: Estrutura para utilizar as querys da application para consulta na base de dados, tanto os commands quanto querias fazem parte do padrão arquitetura CQRS.
- Validators: Estrutura para realizar validações por meio dos fluents validations.

  
