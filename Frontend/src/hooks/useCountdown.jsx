import { useEffect, useState } from "react";

const useCountdown = (logIt = false, target = 10) => {
	const [count, setCount] = useState(target);
	logIt && console.log(count);

	useEffect(() => {
		const intervalX = setInterval(() => {
			setCount((prev) => {
				if (prev < 1) {
					clearInterval(intervalX);
					return 0;
				} else {
					return prev - 1;
				}
			});
		}, 1000);

		return () => clearInterval(intervalX);
	}, []);
	return { count };
};

export default useCountdown;
