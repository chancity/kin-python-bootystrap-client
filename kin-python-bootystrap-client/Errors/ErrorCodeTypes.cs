namespace kin_python_bootystrap_client.Models
{
    public enum ErrorCodeTypes
    {
        InternalError = 500,
        InvalidParamError = 4001,
        DestinationDoesNotExistError = 4002,
        LowBalanceError = 4003,
        InvalidTransactionError = 4004,
        CantDecodeTransactionError = 4005,
        MissingParamError = 4006,
        ExtraParamError = 4007,
        InvalidBodyError = 4008,
        DestinationExistsError = 4009,
        NotFoundError = 4041,
        UnableToDecodeTransactionEnvelope = 4005
    }
}