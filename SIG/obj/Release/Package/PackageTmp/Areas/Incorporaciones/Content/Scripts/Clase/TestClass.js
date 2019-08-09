'use strict'

class TestClass
{

    constructor(CodError , TyError , dError , rAfectados)
    {
        this.Error_Cod = CodError;
        this.TipoError = TyError;
        this.DescripcionError = dError;
        this.RegistrosAfectados = rAfectados
    }


    MostarDescripcionError()
    {
        switch (this.Error_Cod || this.RegistrosAfectados)
        {
            case "0": swal(
                                "",
                                "Esta Persona no es miembro de hogar; se realizará el levantamiento de una fichas socioeconómica única",
                                "Error"
                          );
                this.RegresarActualizar = false;
                break;
            case "1" || false : swal(
                                   "Aviso",
                                   "La información de los miembros del hogar no ha cambiado ya que las agregaciones y el cambio de titular se hacen de forma temporal",
                                   "warning"
                                 );
                this.RegresarActualizar = true;
                break;
            case "1" || true: swal(
                                      "",
                                      "La actualización se realizó correctamente",
                                      "success"
                                  );
                this.RegresarActualizar = true;
                break;
            case "3": this.MostarErrorDiv(this.DescripcionError)
                this.RegresarActualizar = false;
                break;
            case "4": swal(
                                   "Aviso",
                                   "AVISO! Las personas con las identidad(es) : " + this.DescripcionError + " no se van a registra , primero se hara una validación en campo para comprobar su existencia en el hogar.",
                                   "warning"                                       
                                );                                                 
                this.RegresarActualizar = true;                                    
                break;                                                             
            case "5": swal(                                                        
                                   "Aviso",                                        
                                   "AVISO! Las personas con las identidad(es) : " + this.DescripcionError + " no se van a registra debido a que no existente en el mismo hogar en RUP.",
                                   "warning"                                        
                                );                                                  
                this.RegresarActualizar = true;                                     
                break;                                                              
            case "8": swal(                                                         
                                   "Aviso",                                         
                                   "AVISO! Las personas con las identidad(es) : " + this.DescripcionError + " no se puede(n) incorporar al hogar ya que no forman parte de la base de datos del RUP, automaticamente el sistema va hacer una verificación en el CENISS para comprobar su existencia; Las demas actualizaciónes se realizaron correctamente.",
                                   "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "10": swal(
                                  "Aviso",
                                  "La actualización se realizo de forma temporal hasta que se tenga un periodo de actualización abierto",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "25": this.MostarErrorDiv(this.DescripcionError)
                this.RegresarActualizar = false;
                break;
            case "30": swal(
                                  "Aviso",
                                  "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP, los demás cambios se realizaron correctamente.",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "35": swal(
                                  "Aviso",
                                  "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP,.",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "40": swal(
                                 "Aviso",
                                 "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP, Las demas actualizaciones se realizaron de forma temporal  hasta que se tenga un periodo de actualización abierto.",
                                 "warning"
                               );
                this.RegresarActualizar = true;
                break;
            case "45": swal(
                               "Aviso",
                               "Los cambios se realizaron correctamente, las agregaciones o cambio de titular se hicieron de forma temporal",
                               "warning"
                             );
                this.RegresarActualizar = true;
                break;
            case "50": swal(
                               "Aviso",
                               "Las agregaciones o cambio de titular se hicieron de forma temporal, Las demas actualizaciones se realizaron de forma temporal  hasta que se tenga un periodo de actualización abierto.",
                               "warning"
                             );
                this.RegresarActualizar = true;
                break;
            case "55": swal(
                               "Aviso",
                               "Las agregaciones o cambio de titular se hicieron de forma temporal. ya que no se tiene conexion con el CENISS.",
                               "warning"
                             );
                this.RegresarActualizar = true;
                break;
            case "60": swal(
                                  "Aviso",
                                  "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP; el hogar se tendra que validar en campo, los demás cambios se realizaron correctamente.",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "65": swal(
                                  "Aviso",
                                  "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP, el hogar se tendra que validar en campo.",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            case "70": swal(
                                  "Aviso",
                                  "El cambio de titular no se podrá realizar por que la persona no pertenece al mismo hogar en RUP, el hogar se tendra que validar en campo, Las demas actualizaciones se realizaron de forma temporal  hasta que se tenga un periodo de actualización abierto.",
                                  "warning"
                                );
                this.RegresarActualizar = true;
                break;
            default: this.MostarErrorDiv(this.DescripcionError)
                this.RegresarActualizar = false;
                break;

        }

        return this.RegresarActualizar;
    }


    MostarErrorDiv(Descripcion)
    {
        $(".warning").hide()
        $("#Error_text").text(Descripcion)
        $(".error").show()
        $("#Error_text_H").hide()
        $("#Warning_text_H").hide()
    }

  
}
