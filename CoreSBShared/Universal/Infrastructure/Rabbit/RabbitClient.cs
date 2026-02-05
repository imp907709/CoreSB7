using CoreSBShared.Registrations;
using RabbitMQ.Client;

namespace CoreSBShared.Universal.Infrastructure.Rabbit;

public class RabbitClient : IRabbitClient
{
    private RabbitConfig  _config;
    private IConnection _connection;
    private IChannel _cahannel;
    
    public RabbitClient(RabbitConfig config)
    {
        _config = config;
    }

    public async Task<IConnection> CreateConnection()
    {
        ConnectionFactory fac = new ConnectionFactory()
        {
            UserName = _config.User,
            Password = _config.Password,
            HostName = _config.Host,
        };

        this._connection = await fac.CreateConnectionAsync();
        return this._connection;
    }
    
    public async Task<IChannel> ChannelOpen()
    {
        this._cahannel = await this._connection.CreateChannelAsync();
        return this._cahannel;
    }

    public async Task Disconnect()
    {
        await this._cahannel.CloseAsync();
        await this._connection.CloseAsync();
        await this._cahannel.DisposeAsync();
        await this._connection.DisposeAsync();
    }
    public async Task Init()
    {
        await CreateConnection();
    }
}
