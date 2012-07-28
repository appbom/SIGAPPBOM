
var nroArticulos = $("#NumeroItems").val();
var msgAlert = "";
var nroArticulosEnviados = 0;
$("#Descripcion").focus();
//FUNCIONES ALERT---------------------------------------//
showAlert();
$("#msg-alert-close").click(function () {
    $("#msg-alert").hide();
});

function showAlert(msg) {
    $("#msg-alert").hide();
    if (msg == "") {
        $("#msg-alert").hide();
        $("#msg-alert-message span").each(function (index) {
            var contenido = $(this).html();
            if (contenido != "") {
                $("#msg-alert").show();
                //alert(contenido);
            }

        });
    } else {
        $("#msg-alert-message-custom").html(msg);
        $("#msg-alert").show();
    }

}
function ClearAlertCustom() {
    $("#msg-alert-message-custom").html("");
}
$("#msg-alert-close").click(function () {
    $("#msg-alert").hide();
})
//FIN FUNCIONES ALERT---------------------------------------//

//FUNCIONES DETALLES----------------------------------------//
CalcularItems();
function DelItem(nro) {
    $("#art-" + nro).hide();
    $("#estado-" + nro).val("0");
    CalcularItems();

}
var ArticulosId = new Array();
function CalcularItems() {
    var item = 0;
    ArticulosId = new Array();
    $("#detalles-pedido tr").each(function (index) {
        estado = $("#estado-" + index).val();
        if (estado != "0") {
            ArticulosId.push($("#articuloid-" + index).val())
            item++;
            $(this).children("td:first").html(item);
        }
    });
    $("#NumeroItems").val(item);
    showAlert("");

}
function ExisteArticulo(id) {
    for (var i = 0; i < ArticulosId.length; i++) {
        if (ArticulosId[i] == id)
            return true;
    }
    return false;
}
//FINFUNCIONES DETALLES----------------------------------------//

//GRABAR PEDIDO-------------------------------------------//
GenerarDetalles();
function GenerarDetalles() {
    $("#Detalles_Count").val("");
    $("#Estado").val("1");
    $("#hiddens").html("");
    nroArticulosEnviados = 0;
    for (var i = 0; i < nroArticulos; i++) {
        $("#nombre-" + i).val();
        activo = $("#estado-" + i).val();
        if (activo == 1) {
            var itemHidden = '<input type="hidden" name="Detalles[' + nroArticulosEnviados + '].ArticuloNombre" value="' + $("#articulo-" + i).val() + '" /> ';
            itemHidden += '<input type="hidden" name="Detalles[' + nroArticulosEnviados + '].ArticuloId" value="' + $("#articuloid-" + i).val() + '" /> ';
            itemHidden += '<input type="hidden" name="Detalles[' + nroArticulosEnviados + '].CantidadSolicitada" value="' + $("#cantidad-" + i).val() + '" /> ';
            itemHidden += '<input type="hidden" name="Detalles[' + nroArticulosEnviados + '].Item" value="' + (nroArticulosEnviados + 1) + '" /> ';
            if ($("#ItemRegistrado-"+i).val() == "1")
                itemHidden += '<input type="hidden" name="Detalles[' + nroArticulosEnviados + '].Id" value="' + $("#detalleid-" + i).val() + '" /> ';
            $("#hiddens").append(itemHidden);
            nroArticulosEnviados++;
        }

    }
}

$("#btnGrabar").click(function () {
    GenerarDetalles();
    $("#SendForm").click();
    showAlert();

});
//FIN GRABAR PEDIDO-------------------------------------------//



//AGREGAR ARTICULO.----------------------------//

$("#frmNuevoArticulo").hide();

$("#btnNuevoArticulo").click(function () {
    $("#frmNuevoArticulo").show();
    $("#txtArticulo").focus();
})

$("#frmNuevoArticulo-close").click(function () {
    $("#frmNuevoArticulo").hide();
})

$("#ActionSearchArticulo").click(function () {

    $.ajax({
        url: "../ListarArticulos",
        data: {"nombre": $("#ArticuloSearch").val() },
        success: function (result) {

            $("#list-articulos").html(result);
        }

});


});
function SeleccionarArticulo(id, codCatalogo, descripcion) {
    $("#NewArticuloId").val(id);
    $("#NewArticuloCod").val(codCatalogo);
    $("#txtArticulo").val(descripcion);
    $("#myModal-close").click();
    $("#txtCantidad").focus();
}
function ValidaArticulo() {
    var articulo = $("#txtArticulo").val();
    var cantidad = $("#txtCantidad").val();
    if (articulo != "")
        if (!isNaN(parseInt(cantidad)))
            return true;

    return false;
}
$('#btnAgregarArticulo').click(function () {
    ClearAlertCustom();
    CalcularItems();
    if (ValidaArticulo()) {
        if (!ExisteArticulo($("#NewArticuloId").val())) {
            if (!ExisteArticuloOculto()) {
                var itemHidden = '<input type="hidden" id="articulo-' + nroArticulos + '" value="' + $("#txtArticulo").val() + '" /> ';
                itemHidden += '<input type="hidden" id="articuloid-' + nroArticulos + '" value="' + $("#NewArticuloId").val() + '" /> ';
                itemHidden += '<input type="hidden" id="cantidad-' + nroArticulos + '" value="' + $("#txtCantidad").val() + '" /> ';
                itemHidden += '<input type="hidden" id="estado-' + nroArticulos + '" value="1" /> ';
                $("#mensaje").show();
                $("#detalles-pedido").append('<tr id="art-' + nroArticulos + '"><td>' + (nroArticulos + 1) + '</td><td>' + $("#NewArticuloCod").val() + '</td><td>' + itemHidden + $("#txtArticulo").val() + '</td><td>' + $("#txtCantidad").val() + '</td><td><a class="btn" onclick="DelItem(' + nroArticulos + ');"><i class="icon-trash"></i></a></td></tr>');
                $("#txtArticulo").focus();
                nroArticulos++;
                CalcularItems();
                showAlert("");
                $("#txtArticulo").val("");
                $("#txtCantidad").val("");
            }
        } else {
            showAlert("Articulo ya se encuentra en el listado. Seleccione un articulo diferente");
        }
    } else {
        showAlert("Articulo o Cantidad Incorrectos");
    }

})
function ExisteArticuloOculto() {
    var flag = false;
    var id = $("#NewArticuloId").val();
    var articulo = $("#txtArticulo").val();
    var cantidad = $("#txtCantidad").val();
    var codCatalogo = $("#NewArticuloCod").val();
    $("#detalles-pedido tr").each(function (index) {
        estado = $("#estado-" + index).val();
        if (estado == "0") {
            if ($("#articuloid-" + index).val() == id) {
                $("#art-" + index).show();
                $("#estado-" + index).val("1");
                $("#articuloid-" + index).val(id);
                $("#articulo-" + index).val(articulo);
                $("#cantidad-" + index).val(cantidad);
                $(this).children(".row-ArticuloCodCatalogo").html(codCatalogo);
                $(this).children(".row-ArticuloCantidad").html(cantidad);
                flag = true;
            }

        }
    });
    CalcularItems();
    return flag;

}
$("input").keyup(function () {
    showAlert("");
});


//FIN AGREGAR ARTICULO-----------------------------------//