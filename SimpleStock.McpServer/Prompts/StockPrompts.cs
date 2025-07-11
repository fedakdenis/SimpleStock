using System.ComponentModel;
using System.Text;
using ModelContextProtocol.Server;
using Microsoft.Extensions.AI;

namespace SimpleStock.McpServer.Prompts;

[McpServerPromptType]
public static class StockPrompts
{
    [McpServerPrompt]
    [Description("Analyzes warehouse condition and provides inventory management recommendations")]
    public static ChatMessage AnalyzeStock(
        [Description("Additional information about product or category for analysis")] string? focus = null)
    {
        var content = new StringBuilder();
        content.AppendLine(@"Analyze the current warehouse condition and provide recommendations:

1. **Product balance analysis:**
   - Get current balance of all products
   - Identify products with low stock (less than 50 units)
   - Find products with excess inventory (more than 1000 units)

2. **Movement analysis:**
   - Analyze recent product movements
   - Determine most active products
   - Identify trends in receipts and expenses

3. **Recommendations:**
   - Suggest what to order from suppliers
   - Point out products requiring attention
   - Give advice on optimizing warehouse inventory");

        if (!string.IsNullOrEmpty(focus))
        {
            content.AppendLine();
            content.AppendLine($"**Special focus:** {focus}");
        }

        return new ChatMessage(ChatRole.User, content.ToString());
    }

    [McpServerPrompt]
    [Description("Generates report on product movements for specific period")]
    public static ChatMessage GenerateMovementReport(
        [Description("Period start date (YYYY-MM-DD)")] string? startDate = null,
        [Description("Period end date (YYYY-MM-DD)")] string? endDate = null,
        [Description("Specific product for report")] string? product = null)
    {
        var content = new StringBuilder();
        content.AppendLine(@"Create detailed report on warehouse product movements:

1. **Report period:**");

        if (!string.IsNullOrEmpty(startDate) || !string.IsNullOrEmpty(endDate))
        {
            content.AppendLine($"   - From: {startDate ?? "beginning of history"}");
            content.AppendLine($"   - To: {endDate ?? "current date"}");
        }
        else
        {
            content.AppendLine("   - Last 30 days");
        }

        if (!string.IsNullOrEmpty(product))
        {
            content.AppendLine($"   - Focus on product: {product}");
        }

        content.AppendLine(@"

2. **Report structure:**
   - Summary of receipts and expenses
   - Top 5 most active products
   - Analysis of suppliers and recipients
   - Identification of movement anomalies

3. **Conclusions and recommendations:**
   - Main trends
   - Problem areas
   - Improvement suggestions

Get movement data and create structured report.");

        return new ChatMessage(ChatRole.User, content.ToString());
    }

    [McpServerPrompt]
    [Description("Helps with purchase planning based on current stock levels")]
    public static ChatMessage PlanPurchases(
        [Description("Minimum stock level for warning")] int minThreshold = 50,
        [Description("Purchase budget")] decimal? budget = null)
    {
        var content = new StringBuilder();
        content.AppendLine($@"Help plan product purchases:

1. **Current stock analysis:**
   - Get balance of all warehouse products
   - Find products with stock below {minThreshold} units
   - Analyze movement history of these products

2. **Purchase planning:**
   - Calculate average consumption of each product
   - Determine optimal order quantity
   - Prioritize products by criticality");

        if (budget.HasValue)
        {
            content.AppendLine($"   - Stay within budget: {budget:C}");
        }

        content.AppendLine(@"

3. **Recommendations:**
   - List of products for urgent ordering
   - Quantity suggestions
   - Recommended suppliers (based on history)

Create purchase plan with justification for each decision.");

        return new ChatMessage(ChatRole.User, content.ToString());
    }

    [McpServerPrompt]
    [Description("Conducts warehouse operations audit and identifies discrepancies")]
    public static ChatMessage AuditStock()
    {
        var content = @"Conduct warehouse operations audit:

1. **Data integrity check:**
   - Get all product movements
   - Check mathematical balance calculations
   - Identify possible record errors

2. **Anomaly analysis:**
   - Find unusually large product movements
   - Pay attention to negative balances
   - Check logic of receipts/expenses

3. **Data quality:**
   - Check date correctness
   - Ensure required fields are filled
   - Find duplicate records

4. **Audit report:**
   - Summary of found problems
   - Correction recommendations
   - Process improvement suggestions

Provide detailed report with specific findings and recommendations.";

        return new ChatMessage(ChatRole.User, content);
    }
}
