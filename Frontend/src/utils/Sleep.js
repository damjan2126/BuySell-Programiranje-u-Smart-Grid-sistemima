const Sleep = (delay = 2000, success = true) => {
	return new Promise((res, rej) => {
		setTimeout(() => {
			success ? res() : rej("Error message from SLEEP");
		}, delay);
	});
};

export default Sleep;
