function confirmSeries(checkBox, id) {
	const newState = checkBox.checked;
	let message = "Are you sure you want deactivate " + id + ".\n" +
		"This will deactivate all related boosters and cards.\n" +
		"This will hide all related boosters and cards from the users until reactivated.";

	if (newState) {
		message = "Are you sure you want activate " + id + ".\n" +
			"This will activate all related boosters and cards.\n" +
			"This will display all related boosters and cards for the users until deactivated.";
	}

	if (confirm(message)) {
		checkBox.form.submit();
	} else {
		checkBox.checked = !newState;
	}
}

function confirmBooster(checkBox, id) {
	const newState = checkBox.checked;
	let message = "Are you sure you want deactivate " + id + ".\n" +
		"This will deactivate all related cards.\n" +
		"This will hide all related cards from the users until reactivated.";

	if (newState) {
		message = "Are you sure you want activate " + id + ".\n" +
			"This will activate all related cards.\n" +
			"This will display all related cards for the users until deactivated.";
	}

	if (confirm(message)) {
		checkBox.form.submit();
	} else {
		checkBox.checked = !newState;
	}
}

function confirmGeneralActivate(checkBox, id) {
	const newState = checkBox.checked;
	let message = "Are you sure you want deactivate " + id + ".\n";

	if (newState) {
		message = "Are you sure you want activate " + id + ".\n";
	}

	if (confirm(message)) {
		checkBox.form.submit();
	} else {
		checkBox.checked = !newState;
	}
}