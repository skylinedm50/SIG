<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<style>
    .info, .success, .warning, .error, .validation {
        border: 1px solid;
        margin: 10px 0px;
        padding:15px 10px 15px 50px;
        background-repeat: no-repeat;
        background-position: 10px center;
        display: none;
    }
    .info {
        color: #00529B;
        background-color: #BDE5F8;
        background-image: url('../../Areas/Planilla/Images/info.png');
    }
    .success {
        color: #4F8A10;
        background-color: #DFF2BF;
        background-image:url('../../Areas/Planilla/Images/success.png');
    }
    .warning {
        color: #9F6000;
        background-color: #FEEFB3;
        background-image: url('../../Areas/Planilla/Images/warning.png');
    }
    .error {
        color: #D8000C;
        background-color: #FFBABA;
        background-image: url('../../Areas/Planilla/Images/error.png');
    }
</style>
<div id="divMensaje">
    <div class="info"></div>
    <div class="success"></div>
    <div class="warning"></div>
    <div class="error"></div>
</div>