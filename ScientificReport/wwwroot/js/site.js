$(document).ready(function() {
	$('select').formSelect();
	$(".dropdown-trigger").dropdown();

	// Materialize.updateTextFields();
});

$(document).ready(function () {
	$('.sidenav').sidenav();
});

const toggleEntity = (controller, endpoint, tagName) => (tag, id, entityId) => {
	const config = {
		method: 'POST',
		body: JSON.stringify({
			EntityId: entityId,
		}),
		headers: {
			'Content-Type': 'application/json'
		}
	};
	fetch(`/${controller}/${endpoint}/${id}`, config).then(response => {
		console.log(response);
		if (response.status !== 200) throw {msg: "Failed to toggle the entity", response};
		tag.classList.add('hidden');
		tag.parentElement.getElementsByClassName(tagName)[0].classList.remove('hidden');
	}).catch(e => {
		console.error(e);
	});
};

const toggleCheckboxEntity = (controller, endpointAdd, endpointRemove, requestBodyFunc) => (tag, id, entityId) => {
	const config = {
		method: 'POST',
		body: JSON.stringify(requestBodyFunc(entityId)),
		headers: {
			'Content-Type': 'application/json'
		}
	};
	let endpoint = tag.checked === false ? endpointRemove : endpointAdd;
	fetch(`/${controller}/${endpoint}/${id}`, config).then(response => {
		console.log(response);
		if (response.status !== 200) throw {msg: "Failed to toggle the entity", response};
	}).catch(e => {
		console.error(e);
	});
};
