# RPG_Assistant

Ferramenta de linha de comando (CLI) em **C#** para ajudar iniciantes em RPG de mesa a criar sua primeira
ficha de personagem no sistema **Tormenta 20** (RPG brasileiro).
> Projeto de estudo de C# e peça de portfólio, construído para praticar arquitetura, organização e boas
> práticas da linguagem enquanto se aprende na prática.

---

## Sobre o projeto

O RPG_Assistant nasceu da vontade de praticar C# num problema real: ajudar quem nunca jogou RPG a montar
uma ficha jogável de Tormenta 20 sem se perder nas regras. Os dados do sistema (classes, origens, raças,
poderes, magias, itens) vêm dos livros oficiais e são transcritos para arquivos **JSON** que a aplicação
carrega e apresenta de forma guiada.
O foco é a **estrutura de software**, como
organizar um projeto CLI em camadas coesas, evitando tanto o *over-engineering* quanto a bagunça.

---

## Funcionalidades (atual)

- [x] Criação guiada de personagem via terminal (nome, classe, raça, origem, atributos)
- [x] Seleção genérica de opções (única e múltipla) reaproveitável entre todas as escolhas
- [x] Carga de dados a partir de JSON, com cache em memória
- [x] Hierarquia de itens polimórfica (`Item` → `Weapon`, `Armor`, `Shield`, `Ammo`)
- [x] Proteção contra duplicidade em duas camadas (interface + modelo)
- [x] Início das validações de inventário

### Roadmap (MVP)

- [ ] Fluxo de escolha de poderes
- [ ] Fluxo de escolha de magias
- [ ] Fluxo de inventário
- [ ] Level up e seus gatilhos automáticos
- [ ] Regras de multiclasse
- [ ] Aplicação de atributos e habilidades raciais ao personagem
- [ ] Tela final de ficha completa

---

## Stack

| | |
|---|---|
| **Linguagem** | C# (.NET `net10.0`) |
| **Interface** | CLI (terminal), sem GUI |
| **Dados** | Arquivos JSON em `Data/`, carregados e cacheados em memória |
| **Testes** | A definir |

---

## Como executar

Pré-requisito: [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
# clone
git clone https://github.com/Thiago-Godoy-Cunha/RPG_Assistant.git
cd RPG_Assistant

# restaure e rode
dotnet restore
dotnet run --project RPG_Assistant.csproj
```

> Os arquivos JSON de dados ficam em `Data/` e são lidos em tempo de execução.

---

## Estrutura do projeto

O código é organizado por **responsabilidade**, não por tipo de arquivo. O critério para colocar código
em cada pasta:

| Pasta | Responsabilidade |
|---|---|
| `Cli/` | Interação com o usuário via `Console` (prompts, menus, fluxos de tela) |
| `Rules/` | Regras de jogo puras, testáveis isoladamente, sem dependência de `Console` |
| `Loading/` | Leitura e cache de dados a partir dos arquivos JSON |
| `Models/` | Entidades de domínio (dados), sem lógica de I/O |
| `Config/` | Configuração central (caminhos de arquivo etc.) |
| `Enums/` | Tipos fechados do domínio (`ClassType`, `ItemType`, `SpellCircle`...) |
| `Extensions/` | Utilidades genéricas reaproveitáveis |
| `Data/` | Arquivos `.json` de dados do jogo |
| `Program.cs` | Ponto de entrada fino, delega para um `Cli/*Flow.cs` |

**Regra prática:** se toca `Console` → `Cli/`; se calcula/decide regra sem tela → `Rules/`; se define dado
→ `Models/`/`Enums/`; se lê arquivo → `Loading/`.

---

## Decisões de arquitetura (e por quê)

Este projeto vive num meio-termo deliberado, **sem over-engineering, sem desorganização**:

- **Sem DI formal / `IRepository<T>` / camada de serviço separada** — peso desnecessário para um projeto
  CLI solo. Reavaliado se o cenário mudar.
- **Loaders explícitos, sem inferência por reflexão** — o caminho e o nome da propriedade raiz são
  informados explicitamente. A explicitação evita erros que só apareceriam em runtime.
- **Factory Method embutido, sem classe `Factory`** — subclasses (ex.: `Weapon`) são construídas por um
  método estático na própria classe base (`Item.Create`), e os construtores usados só pela fábrica são
  `internal`.
- **Fail-fast em dado malformado** — campos obrigatórios ausentes lançam `InvalidDataException`; `?? default`
  só quando nulo é aceitável no domínio.
- **Dois tipos de JSON, dois parse** — os arquivos de dados não seguem um formato único (alguns são array
  de entidades, outros usam wrappers de categoria). Cada formato tem seu parse específico, reaproveitando o
  que já existe.

---

## Contribuindo / Feedback

Este é um projeto de aprendizado e portfólio. Feedback técnico e de regras é muito bem-vindo. Abra uma
*issue* ou *pull request*. Para dúvidas sobre o sistema Tormenta 20, consulte os livros oficiais.
