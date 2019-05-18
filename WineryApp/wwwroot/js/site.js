$(function () { //nakon što je stranica učitana
	$(document).on("click", ".delete", function (event) {
		if (!confirm("Obrisati zapis?")) {
			event.preventDefault();
		}
	});
});

//Skripta za stvaranje tablice pomoću JQuery DataTables plugina
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