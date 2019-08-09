﻿<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    /*body
    {
        font-family: Arial;
        font-size: 10pt;
    }*/
    .modal
    {
        position: fixed;
        z-index: 999;
        height: 100%;
        width: 100%;
        top: 0;
        left: 0;
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
        -moz-opacity: 0.8;
    }
    .center
    {
        z-index: 1000;
        margin: 300px auto;
        padding: 10px;
        width: 130px;
        /*background-color: White;*/
        border-radius: 10px;
        filter: alpha(opacity=100);
        opacity: 1;
        -moz-opacity: 1;
    }
    .center img
    {
        height: 128px;
        width: 128px;
    }
</style>
<div class="modal" style="display: none">
    <div class="center">
        <img alt="" src="<%: ResolveUrl("~/Areas/Mineria/images/loader.gif")%>" />
    </div>
</div>

