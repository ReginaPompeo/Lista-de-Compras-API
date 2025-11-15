# 🛒 Lista de Compras — Backend (API .NET 8 + SQLite)

Este repositório contém o **backend** da aplicação Lista de Compras, desenvolvido em **ASP.NET Core 8**, com persistência em **SQLite** e deploy no **Render**.

A API oferece operações completas de CRUD para gerenciamento de itens da lista de compras.

---

## 🚀 Tecnologias Utilizadas
- **C# / .NET 8 Web API**
- **Entity Framework Core**
- **SQLite**
- **EF Core Migrations**
- **Swagger UI**
- **Docker**
- **Render (deploy)**

---

## 🧱 Arquitetura
- Padrão **Controller + DbContext + Migrations**
- Injeção de dependência configurada (AppDbContext)
- Estrutura simples e escalável
- CORS liberado para acesso do frontend hospedado no Netlify

---

## 🗄️ Banco de Dados
- Banco: **SQLite**
- Arquivo gerado automaticamente: `ListaDeCompras.db`
- Migrations configuradas para versionamento do esquema

### **Modelo Principal**
```csharp
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public bool Purchased { get; set; }
}
```
## 📦 Por que este projeto usa um Dockerfile?

O uso do **Dockerfile** foi essencial para o deploy no Render. Aqui estão os **3 motivos principais**:

---

### **1️⃣ Garantir compatibilidade com o ambiente de runtime**

O Dockerfile permite definir exatamente:

- Versão do **SDK .NET** usada para compilar  
- Versão do **Runtime .NET** usada para rodar  
- Dependências incluídas  
- Estrutura de pastas  

Isso evita erros comuns como:

- *"Failed to find .NET runtime"*
- *"Unable to publish project"*

---

### **2️⃣ Permitir que o SQLite funcione corretamente dentro do container**

O SQLite gera arquivos físicos:

- `.db`
- `.wal`
- `.shm`

O Dockerfile garante:

- Uma pasta válida com permissão de escrita/leitura  
- Persistência dos arquivos dentro do container  
- Caminho correto para inicialização do banco  

Sem Docker, o Render pode falhar ao tentar inicializar o SQLite.

---

### **3️⃣ Controle total sobre o processo de build e execução**

Com Dockerfile, você define exatamente como o projeto é construído e iniciado:

```bash
dotnet restore
dotnet build
dotnet publish
dotnet ListaDeCompras.dll
```

Isso evita problemas como:

- "No project found"
- "Build failed"
- "ASP.NET Core runtime not detected"

Garantindo que o deploy funcione toda vez, sem surpresas.

---

## 🌐 Endpoints Disponíveis

| Método | Rota               | Descrição                     |
|--------|----------------------|---------------------------------|
| GET    | /api/itens           | Lista todos os itens            |
| GET    | /api/itens/{id}      | Retorna um item específico      |
| POST   | /api/itens           | Cria um novo item               |
| PUT    | /api/itens/{id}      | Atualiza um item existente      |
| DELETE | /api/itens/{id}      | Remove um item                  |

Swagger disponível para testes locais.

---

## ☁️ Deploy no Render

O deploy foi realizado usando:

- Dockerfile customizado  
- Container com SQLite persistido  
- Build automático a cada push  

### 🌎 API pública:
[https://lista-de-compras-api-hi0w.onrender.com/api/itens](https://lista-de-compras-api-hi0w.onrender.com)

---

## 🧠 Aprendizados

- Criação de API REST com .NET 8  
- Configuração de SQLite + Migrations  
- Deploy profissional com Docker  
- Resolução de conflitos de pacotes  
- Como funciona o SQLite em containers  
- Integração com frontend hospedado no Netlify  
