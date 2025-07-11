# SimpleStock

Warehouse inventory management system with AI assistant

## Project Description

SimpleStock is a demonstration project showcasing practical implementation of Model Context Protocol in the .NET ecosystem. The project demonstrates how to integrate AI with business logic through the MCP protocol.

**The project consists of:**
- **Web API** (.NET 8) - Web application and REST API for warehouse inventory management
- **MCP Server** - Model Context Protocol server for AI integration
- **Chat Console** - Console application for AI interaction through chat
- **Ollama** - Local AI model for request processing

### Quick Demo Setup

#### Prerequisites
- Docker
- Docker Compose

#### Launch Demo Project

1. Clone the repository:
```bash
git clone https://github.com/fedakdenis/SimpleStock.git
cd SimpleStock
```

2. Start all services:
```bash
docker-compose up -d
```

3. Wait for all containers to start (may take several minutes):
```bash
docker-compose ps
```

#### Testing AI Assistant

To interact with AI through console:

```bash
docker-compose exec simplestock-chat dotnet SimpleStock.Chat.dll
```

**Natural Language Interaction**

The AI assistant allows you to interact with the system using natural language. You can ask questions, give commands, and even make requests with typos or imprecise wording - the AI will understand and process your requests intelligently.

**Example 1: Inventory Query**
```
You >> Show stock balance
SimpleStock >> Here is the current stock balance:

- **3M Post-it Notes Variety Pack**: 120 pcs.
- **Canon imageCLASS Printer**: 20 pcs.
- **Cisco Catalyst 9300 Switch**: 12 pcs.
- **Dell OptiPlex 7090 Desktop**: 12 pcs.
- **Dell UltraSharp 27" 4K Monitor**: 14 pcs.
- **Fellowes Bankers Box Storage**: 60 pcs.
- **Herman Miller Aeron Chair**: 40 pcs.
- **HP EliteBook 850 G9 Laptop**: 12 pcs.
- **IKEA BEKANT Standing Desk**: 30 pcs.
- **iPad Pro 12.9-inch M2**: 30 pcs.
- **Logitech MX Master 3S Mouse**: 55 pcs.
- **MacBook Pro**: 5 pcs.
- **MacBook Pro 16-inch M3**: 4 pcs.
- **Microsoft Surface Studio 2+**: 4 pcs.
- **NETGEAR Nighthawk Pro Gaming Router**: 23 pcs.
- **Razer BlackWidow V4 Pro Keyboard**: 30 pcs.
- **Samsung Galaxy Tab S9 Ultra**: 27 pcs.
- **Ubiquiti UniFi Access Point**: 35 pcs.

**Total items:** 533 pcs.
```

**Example 2: Ambiguous Request Handling**
```
You >> Add couple laptops to Developers team
SimpleStock >> Sure, I will add a few MacBook Pros to the Developers team. Let's add 5 MacBook Pros for now.

Here are the details of the stock movement:
- **Type**: Expense
- **Product**: MacBook Pro
- **Quantity**: 5 pcs.
- **Recipient**: Developers team

The expense has been successfully added.

Would you like to perform any other actions or have more items added?
```

**Example 3: Typo Tolerance**
```
You >> Buy 10 MacBook in Aplle Store
SimpleStock >> The purchase of 10 MacBook Pros from the Apple Store has been successfully recorded as an income.

Here are the details:
- **Type**: Income
- **Product**: MacBook Pro
- **Quantity**: 10 pcs.
- **Supplier**: Apple Store

If you need to perform any further actions or have more questions, feel free to let me know!
```

**Example 4: Natural Language Queries**
```
You >> How many laptops do we have on the store?
SimpleStock >> To provide you with an accurate count of laptops in stock, I will fetch the current balance for all laptop models.

Here are the current inventory levels for laptops:

- **MacBook Pro**: 15 pcs.
- **Dell OptiPlex 7090 Desktop**: 12 pcs.
- **HP EliteBook 850 G9 Laptop**: 12 pcs.
- **Microsoft Surface Studio 2+**: 4 pcs.

**Total laptops:** 43 pcs.

If you need more detailed information or another specific product, please let me know!
```