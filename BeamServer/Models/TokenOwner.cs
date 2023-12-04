using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace BeamServer.Models.TokenOwner
{


    public partial class TokenOwnerDeployment : TokenOwnerDeploymentBase
    {
        public TokenOwnerDeployment() : base(BYTECODE) { }
        public TokenOwnerDeployment(string byteCode) : base(byteCode) { }
    }

    public class TokenOwnerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public TokenOwnerDeploymentBase() : base(BYTECODE) { }
        public TokenOwnerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetOwnersFunction : GetOwnersFunctionBase { }

    [Function("getOwners", "address[]")]
    public class GetOwnersFunctionBase : FunctionMessage
    {
        [Parameter("address", "_contract", 1)]
        public virtual string Contract { get; set; }
        [Parameter("uint256[]", "ids", 2)]
        public virtual List<BigInteger> Ids { get; set; }
    }

    public partial class GetOwnersOutputDTO : GetOwnersOutputDTOBase { }

    [FunctionOutput]
    public class GetOwnersOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "owners", 1)]
        public virtual List<string> Owners { get; set; }
    }
}


