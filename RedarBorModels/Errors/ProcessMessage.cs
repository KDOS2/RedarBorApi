namespace RedarBorModels.Errors
{
    public static class ProcessMessage
    {
        public const string addNewItemError = "Error al intentar adicionar un nuevo registro";
        public const int addNewItemId = 1001;
        public const string addNewItemSucces = "Registro insertado satisfactoriamente";

        public const string updateItemError = "Error al intentar actualizar el registro";
        public const int updateItemId = 1002;
        public const string updateItemSucces = "Registro actualizado satisfactoriamente";

        public const string deleteItemError = "Error al intentar eliminar el registro";
        public const int deleteItemId = 1003;
        public const string deleteItemSucces = "Registro eliminado satisfactoriamente";

        public const string addUpdateItemfacadeParamsError = "Un parametro que es obligatorio tiene valor nulo o no permitido para su registro";
        public const string addUpdateItemEmailfacadeParamsError = "El campo email tiene un foramto erroneo";
        public const int addUpdateItemfacadeParamsId = 1020;

        public const string getTokenfacadeParamsError = "Flatan datos para la generacion del token";
        public const int getUpdateTokefacadeParamsId = 1021;

        public const string getItemSucces = "Consulta generada satisfactoriamente";
        public const int getItemId = 1010;
        public const string getItemError = "Error al realizar el proceso de consulta";

        public const string getTokenSucces = "Token generado exitosamente";
        public const int getTokenId = 2001;
        public const string getTokenError = "Error al generar el token";
    }
}
