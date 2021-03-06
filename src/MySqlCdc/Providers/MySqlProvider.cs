using System.Threading.Tasks;
using MySqlCdc.Commands;
using MySqlCdc.Events;
using MySqlCdc.Network;
using MySqlCdc.Protocol;
using MySqlCdc.Providers.MySql;

namespace MySqlCdc.Providers
{
    public class MySqlProvider : IDatabaseProvider
    {
        public EventDeserializer Deserializer { get; } = new MySqlEventDeserializer();

        public async Task DumpBinlogAsync(DatabaseConnection channel, ConnectionOptions options)
        {
            long serverId = options.Blocking ? options.ServerId : 0;
            ICommand command = null;

            if (options.Binlog.StartingStrategy == StartingStrategy.FromGtid)
            {
                command = new DumpBinlogGtidCommand(serverId, options.Binlog.Filename, options.Binlog.Position, options.Binlog.Gtid);
            }
            else
            {
                command = new DumpBinlogCommand(serverId, options.Binlog.Filename, options.Binlog.Position);
            }

            await channel.WriteCommandAsync(command, 0);
        }
    }
}
