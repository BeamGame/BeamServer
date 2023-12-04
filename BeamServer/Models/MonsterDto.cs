using System;
namespace BeamServer.Models
{
	public class AddMonsterDto
	{

		public string Name { get; set; }
        public int Level { get; set; }
    }

    public class UpdateMonsterDto
    {

        public int MonsterId { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
    }

    public class TransferMonsterDto
    {
        public int MonsterId { get; set; }
        public string UserName { get; set; }
    }

    public class SendMonsterDto
    {
        public string MonsterId { get; set; }
        public string ReceiverAddress { get; set; }
    }
}

