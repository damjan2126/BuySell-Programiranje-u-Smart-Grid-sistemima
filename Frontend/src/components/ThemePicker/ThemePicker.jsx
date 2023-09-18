import React, { useCallback, useEffect, useState } from "react";
import Theme from "../../themes/Theme";
import {
	ThemePickerContainer,
	ThemePickerItem,
	PickedText,
} from "./ThemePicker.styled";
import { useDispatch, useSelector } from "react-redux";
import {
	changeTheme,
	themeSliceState,
} from "../../store/features/themeSlice/themeSlice";
import SingleThemeItem from "./SingleThemeItem";

const ThemePicker = () => {
	const [themes, setThemes] = useState([]);
	const dispatch = useDispatch();
	const themeState = useSelector(themeSliceState);

	const getThemeNames = useCallback(() => {
		const themes = [];
		for (let [id, value] of Object.entries(Theme)) {
			themes.push({ id, value });
		}
		setThemes(themes);
	}, []);

	const mappedThemes = themes.map((el, index) => {
		return (
			<ThemePickerItem
				$styles={el.value}
				selected={themeState === el}
				key={index}
				onClick={() => dispatch(changeTheme(el.id))}>
				<SingleThemeItem value={el.value} name={el.id} />
				{themeState === el.id && (
					<PickedText $styles={el.value}>Picked</PickedText>
				)}
			</ThemePickerItem>
		);
	});

	useEffect(() => {
		getThemeNames();
	}, [getThemeNames]);

	return <ThemePickerContainer>{mappedThemes}</ThemePickerContainer>;
};

export default ThemePicker;
