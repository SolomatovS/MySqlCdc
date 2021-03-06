namespace MySqlCdc.Events
{
    /// <summary>
    /// Last event in a binlog file which points to next binlog file.
    /// Fake version is also returned when replication is started.
    /// <see cref="https://mariadb.com/kb/en/library/rotate_event/"/>
    /// </summary>
    public class RotateEvent : BinlogEvent
    {
        public string BinlogFilename { get; }
        public long BinlogPosition { get; }

        public RotateEvent(EventHeader header, string binlogFilename, long binlogPosition)
            : base(header)
        {
            BinlogFilename = binlogFilename;
            BinlogPosition = binlogPosition;
        }
    }
}
