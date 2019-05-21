$(function () { //nakon što je stranica učitana
	$(document).on("click", ".delete", function (event) {
		if (!confirm("Obrisati zapis?")) {
			event.preventDefault();
		}
	});
});

// #region Skripta za stvaranje tablice pomoću JQuery DataTables plugina
$(document).ready(function() {
	$("#tablica").DataTable({
		"aoColumnDefs": [
			{
				"bSortable": false, //zadnji stupac nema sort (jer su nam to ikonice)
				"aTargets": [-1]
			}
		],
		language: {
			search: "Pretraga",
			lengthMenu: "Prikaži _MENU_ zapisa",
			paginate: {
				first: "Prva",
				previous: "Prethodna",
				next: "Sljedeća",
				last: "Zadnja"
			},
			emptyTable: "Nema podataka za prikaz",
			info: "_START_ - _END_ od ukupno _TOTAL_ zapisa",
			infoEmpty: "Nema podataka za prikaz",
			infoFiltered: "(filtrirano od ukupno _MAX_ zapisa)"
		}
	});
});

// #endregion


$("#spremnikSelect").prop("disabled", true);

$("#podrumSelect").change(async function () {
    var selectedPodrum = $(this).val();

    if (selectedPodrum == "") {                 //ako smo odabrali općenito
	    $("#spremnikSelect").prop("disabled", true);
    } else {
	    var spremnici = await $.ajax(`/Spremnici/GetSpremniciPodruma?idPodrum=${selectedPodrum}`);
        $("#spremnikSelect").html(spremnici);
        $("#spremnikSelectEdit").html(spremnici);
	    $("#spremnikSelect").prop("disabled", false);
    }
});

$("#spremnikSelectAnaliza").prop("disabled", true);

$("#podrumSelectAnaliza").change(async function () {
    var selectedPodrum = $(this).val();

	var spremnici = await $.ajax(`/RezultatAnalize/GetSpremniciPodruma?idPodrum=${selectedPodrum}`);
    $("#spremnikSelectAnaliza").html(spremnici);
    $("#spremnikSelectAnalizaEdit").html(spremnici);
	$("#spremnikSelectAnaliza").prop("disabled", false);
});

$("#popunjeno").change(function () {
    if ($(this).is(":checked")) {
	    $("#napunjenostInput").prop("disabled", false);
		$("#berbaInput").prop("disabled", false);
		$("#sortaInput").prop("disabled", false);
		$("#punilacInput").prop("disabled", false);
	}
});
$("#prazno").change(function () {
    if ($(this).is(":checked")) {
	    $("#napunjenostInput").prop("disabled", true);
		$("#berbaInput").prop("disabled", true);
		$("#sortaInput").prop("disabled", true);
        $("#punilacInput").prop("disabled", true);
	}
});

$("#vrstaSpremnikaSelect").change(async function() {
    var selectedVrsta = $(this).val();
    var opisVrste = await $.ajax(`/Spremnici/GetOpisVrsteSpremnika?idVrsta=${selectedVrsta}`);

    $("#spremnikOpis").html(opisVrste);

});


// #region ZADACI ODABIR KATEGORIJE DODAVANJE ADITIVA

if ($("#kategorijaZadatkaSelect").val() != 4) {
	$("#vrstaAditivSelectInput").hide();
	$("#aditivSelectInput").hide();
	$("#iskorištenaKoličina").hide();
}

$("#statusZadatkaInput").change(async function() {
    var selectedStatus = $(this).val();

    if (selectedStatus == 1) {
	    $("#iskorištenaKoličinaEdit").prop("disabled", false);
    } else {
	    $("#iskorištenaKoličinaEdit").prop("disabled", true);
    }
});

$("#kategorijaZadatkaSelect").change(async function() {
    var selectedKategorija = $(this).val();

    if (selectedKategorija == 4) { //ako smo odabrali dodavanje aditiva
        $("#vrstaAditivSelectInput").show();
        $("#iskorištenaKoličina").show();
    } else {
        $("#vrstaAditivSelectInput").hide();
        $("#aditivSelectInput").hide();
        $("#iskorištenaKoličina").hide();
    }
});

$("#vrstaAditivSelectInput").change(async function () {
    var selectedKategorija = $("#vrstaAditivSelectInput .custom-select").val();

    var aditivi = await $.ajax(`/Aditivi/GetAditivi?idVrstaAditiva=${selectedKategorija}`);
    $("#aditivSelectInput").show();
    $("#aditivSelectInput .custom-select").html(aditivi);
});

// #endregion
