# Mulletaflix

> Servidor de mídia fork do Jellyfin com melhorias e customizações

---

## O que é o Mulletaflix?

O Mulletaflix é uma versão customizada e turbinada do Jellyfin, um servidor de mídia gratuito e de código aberto. Este projeto adiciona:

- **Melhorias de performance** - Queries otimizadas, cache, interceptadores
- **Segurança reforçada** - Hardening de segurança, prevenção de injection
- **Arquitetura limpa** - Thin Controllers, Clean Architecture
- **Code quality** - Supressão de warnings, código limpo

---

## Stack Tecnológico

- **Backend**: .NET 10 / ASP.NET Core
- **Database**: SQLite com EF Core 10
- **Media Server**: FFmpeg
- **Frontend**: Emby-web (embutido)

---

## Como Compilar

```bash
# Restaurar dependências
dotnet restore Jellyfin.sln

# Compilar solução completa
dotnet build Jellyfin.sln
```

---

## Estrutura do Projeto

```
Mulletaflix/
├── Jellyfin.Api/         # API REST
├── Jellyfin.Server/      # Host do servidor
├── MediaBrowser.*       # Bibliotecas principais
├── Emby.Server.Implementations/  # Implementações
├── src/                  # Módulos adicionais
└── tests/                # Testes unitários
```

---

## Documentação

A documentação completa está no Obsidian:
- `docs/Obsidian/UmbrellaPowerTech/Projetos/Mulletaflix/`

---

## Contribuindo

1. Fork o repositório
2. Crie uma branch para sua feature (`git checkout -b feature/nova`)
3. Commit suas mudanças (`git commit -m 'Add nova feature'`)
4. Push para a branch (`git push origin feature/nova`)
5. Crie um Pull Request

---

## Licença

GPL-2.0 - Same as Jellyfin

---

*Projeto baseado no Jellyfin - https://jellyfin.org*
*Fork desenvolvido por Raphael Pereira*