using FluentValidation.Results;
using System.Collections.Generic;

namespace Ecommerce.Transversal.Common
{

    //esta clase va a contener toda la informacion que va a disponer los diversos recursos de la web api
    public class ResponseGeneric<T>
    {

        //esta propiedad almancenará la informacion de  los diversos metodos de la capa de dominio
        //va almacenar la respuesta del metodo update, delete, insert etc...
        public T Data { get; set; }

        //este atributo va alamcenar la informacion del estado de la ejecucion
        //por ejemplo si las llamadas a una base de datos se ejecutan bien devolveremos true en caso contrario false
        public bool IsSuccess { get; set; }

        //aqui almacenara la informacion del mensaje si la ejecucion fue correcta, si fue incorrecta
        //almacenará los detalle del try catch
        public string Message { get; set; }

        public IEnumerable<ValidationFailure> Errors { get; set; }  //ValidationFailure es un objeto propio de fluent validation
    }
}
