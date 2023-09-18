import React, {
	Children,
	cloneElement,
	useEffect,
	useMemo,
	useRef,
	useState,
} from "react";
import { createPortal } from "react-dom";
import {
	Dialog,
	DialogInnerHolder,
	DialogContent,
	DialogChildren,
	DialogHeadline,
	DialogOpenButton,
	DialogButtonHolder,
} from "./Model.styled";
import { Button } from "../Button/Button.styled";

const Modal = ({
	children,
	button,
	headline = "Default Headline",
	borderradius,
	passesProps = false,
	...rest
}) => {
	const [isOpen, setIsOpen] = useState(false);
	const dialogRef = useRef(null);

	const openModal = () => setIsOpen(true);
	const closeModal = () => setIsOpen(false);

	useEffect(() => {
		if (isOpen) dialogRef.current.showModal();
		else dialogRef?.current?.close();
	}, [isOpen]);

	useEffect(() => {
		const func = (e) => e.key === "Escape" && closeModal();
		window.addEventListener("keydown", func);
		return () => window.removeEventListener("keydown", func);
	}, []);

	const myChildren = useMemo(() => {
		return passesProps
			? Children.map(children, (child) => {
					return cloneElement(child, { closeModal });
			  })
			: children;
	}, [passesProps, children]);

	return (
		<>
			<DialogOpenButton onClick={openModal}>{button}</DialogOpenButton>
			{isOpen &&
				createPortal(
					<Dialog {...rest} ref={dialogRef} borderradius={borderradius}>
						<DialogInnerHolder>
							<DialogContent>
								<DialogHeadline>{headline}</DialogHeadline>
								<DialogChildren>{myChildren}</DialogChildren>
							</DialogContent>
							<DialogButtonHolder>
								<Button onClick={closeModal}>Close</Button>
							</DialogButtonHolder>
						</DialogInnerHolder>
					</Dialog>,
					document.getElementById("modal")
				)}
		</>
	);
};

export default Modal;
