let searchRequest = (pSubstring, pType) => {
	$.getJSON('/Publication/SearchPublications', {
		substring: pSubstring,
		type: pType
	}, function (response) {
		if (response.success !== true) {
			console.log({msg: 'Failed publications search', response});
		} else {
			console.log(response['publications']);
			buildDropdown(response['publications']);
		}
	});
};

let inputsChanged = () => {
	let pSubstring = $('#Title').val();
	let pType = $('#Type').val();
	if (pSubstring.length > 0) {
		searchRequest(pSubstring, pType);
	}
};

let buildDropdown = (publications) => {
	let ul = $('#search-result-list');
	ul.empty();
	$('<h6/>')
		.text('Available publications')
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
	ul.removeClass('hide');
};

$(document).ready(function () {
	$('#Title').on('input', function () {
		inputsChanged();
	});

	$('select[id=Type]').on('change', function () {
		inputsChanged();
	});
});
