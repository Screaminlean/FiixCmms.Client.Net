using FiixCmms.Client.Api.Rpc;

namespace FiixCmms.Client.Cli.Examples.Rpc;

/// <summary>
/// Examples for all documented Fiix CMMS RPC methods.
/// Reference: https://fiixlabs.github.io/api-documentation/index.html#/ApiDoc#MethodRpc-Java
/// </summary>
public static class AllRpcMethods
{
    #region Basic Connectivity

    /// <summary>
    /// Ping - Test server connectivity.
    /// </summary>
    public static async Task<RpcResponse> PingAsync(FiixCmmsClient client)
    {
        var request = client.PrepareRpc();
        request.Name = "Ping";
        return await client.RpcAsync(request);
    }

    #endregion

    #region Calendar & Scheduling

    /// <summary>
    /// Calendar - Get calendar information.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetCalendarAsync(FiixCmmsClient client, Dictionary<string, object>? parameters = null)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "Calendar";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// GetTimezone - Get timezone information.
    /// </summary>
    public static async Task<RpcResponse> GetTimezoneAsync(FiixCmmsClient client)
    {
        var request = client.PrepareRpc();
        request.Name = "GetTimezone";
        return await client.RpcAsync(request);
    }

    #endregion

    #region Asset Operations

    /// <summary>
    /// AssetResolved - Get resolved asset information with related data.
    /// Returns paginated results.
    /// Note: Currently returns as dynamic response due to API signature limitations.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetAssetResolvedAsync(
        FiixCmmsClient client,
        Dictionary<string, object>? parameters = null)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "AssetResolved";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// ScheduleTriggerAssetEvent - Trigger asset event scheduling.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> ScheduleTriggerAssetEventAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "ScheduleTriggerAssetEvent";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Scheduled Maintenance Triggers

    /// <summary>
    /// ScheduleTriggerCommon - Common schedule trigger.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> ScheduleTriggerCommonAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "ScheduleTriggerCommon";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// ScheduleTriggerMeterReading - Trigger based on meter readings.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> ScheduleTriggerMeterReadingAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "ScheduleTriggerMeterReading";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// ScheduleTriggerTime - Trigger based on time.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> ScheduleTriggerTimeAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "ScheduleTriggerTime";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Work Order Operations

    /// <summary>
    /// WorkOrderLog - Get work order log entries.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetWorkOrderLogAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "WorkOrderLog";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// FollowOnWorkOrders - Get follow-on work orders.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetFollowOnWorkOrdersAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "FollowOnWorkOrders";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// TaskGroupsToWorkOrder - Convert task groups to work orders.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> TaskGroupsToWorkOrderAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "TaskGroupsToWorkOrder";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Custom Fields

    /// <summary>
    /// CustomFields - Various custom field operations.
    /// Common actions: "getCustomTableData", "getWorkOrderCustomFieldsMetaData"
    /// </summary>
    public static async Task<ParameterizedRpcResponse<T>> CustomFieldsAsync<T>(
        FiixCmmsClient client,
        string action,
        Dictionary<string, object>? parameters = null) where T : class
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "CustomFields";
        request.Action = action;
        request.Parameters = parameters;
        return await client.RpcAsync<T>(request);
    }

    #endregion

    #region Inventory & Stock

    /// <summary>
    /// StocksReceived - Get received stock information.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetStocksReceivedAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "StocksReceived";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Audit & Activity

    /// <summary>
    /// ActivityLog - Get activity log entries.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetActivityLogAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "ActivityLog";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// AuditTrail - Get audit trail information.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetAuditTrailAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "AuditTrail";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Dashboard & Reporting

    /// <summary>
    /// DashboardWidget - Get dashboard widget data.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> GetDashboardWidgetAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "DashboardWidget";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    /// <summary>
    /// DataExport - Export data.
    /// </summary>
    public static async Task<ParameterizedRpcResponse<dynamic>> DataExportAsync(
        FiixCmmsClient client,
        Dictionary<string, object> parameters)
    {
        var request = client.PrepareParameterizedRpc();
        request.Name = "DataExport";
        request.Parameters = parameters;
        return await client.RpcAsync<dynamic>(request);
    }

    #endregion

    #region Site & Access

    /// <summary>
    /// GetAccessibleSites - Get sites accessible to the current user.
    /// </summary>
    public static async Task<RpcResponse> GetAccessibleSitesAsync(FiixCmmsClient client)
    {
        var request = client.PrepareRpc();
        request.Name = "GetAccessibleSites";
        return await client.RpcAsync(request);
    }

    #endregion
}
