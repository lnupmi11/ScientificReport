let searchRequest = (pSubstring, pType) => {
	$.getJSON('/Publication/SearchPublications', {
		substring: pSubstring,
		type: pType
	}, function (response) {
		if (response.success !== true) {
			console.log({msg: 'Failed publications search'});
		} else {
			buildDropdown(response['publications']);
		}
	});
};

let inputsChanged = () => {
	let pSubstring = $('#Title').val();
	let pType = $('#Type').val();
	if (pSubstring.length > 0) {
		searchRequest(pSubstring, pType);
	} else {
		$('#search-result-list').addClass('hide');
	}
};

let buildDropdown = (publications) => {
	let ul = $('#search-result-list');
	ul.empty();
	$('<h6/>')
		.text(availablePublicationsText)
		.appendTo(
		$('<li/>')
			.addClass('collection-header')
			.appendTo(ul)
	);
	for (i in publications) {		
		let li = $('<li/>')
			.addClass('collection-item')
			.appendTo(ul);
		$('<a/>')
			.attr('href', '/Publication/Details/' + publications[i].id)
			.text(publications[i].title)
			.appendTo(li);
	}
	if (publications.length > 0) {
		ul.removeClass('hide');	
	} else {
		ul.addClass('hide');
	}
};

$(document).ready(function () {
	$('#Title').on('input', function () {
		inputsChanged();
	});

	$('select[id=Type]').on('change', function () {
		inputsChanged();
	});
});
