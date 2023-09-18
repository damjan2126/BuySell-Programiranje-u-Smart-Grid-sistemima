import styled from "styled-components";

export const DialogOpenButton = styled.span``;

export const Dialog = styled.dialog`
	border-radius: ${({ borderradius }) => (borderradius ? borderradius : "4px")};
	margin: auto;
	animation: hopInFromBottom 300ms ease-in-out;
	word-break: break-all;
	padding: 20px;
	background-color: var(--hover-light);
	border: none;
	min-width: 300px;
	background-color: ${(props) => props.theme.background};
	color: ${(props) => props.theme.textQuaternary};
	box-shadow: 0 2.7px 9px rgba(0, 0, 0, 0.455),
		0 9.4px 24px rgba(0, 0, 0, 0.315), 0 21.8px 43px rgba(0, 0, 0, 0.28);

	&::backdrop {
		background-color: rgba(0, 0, 0, 0.61);
		animation: fadeIn 300ms linear;
	}

	&::-webkit-scrollbar {
		display: none;
	}

	@media only screen and (max-width: 600px) {
		max-width: 100%;
		max-height: 100%;
		width: 100%;
		height: 100%;
		border-radius: 0px;
	}
`;

export const DialogInnerHolder = styled.div`
	height: 100%;
	width: 100%;

	@media only screen and (max-width: 600px) {
		display: flex;
		height: 100%;
		width: 100%;
		flex-direction: column;
		justify-content: space-between;
	}
`;

export const DialogContent = styled.div``;

export const DialogHeadline = styled.div`
	margin-bottom: 30px;
	text-align: center;
`;

export const DialogChildren = styled.div``;

export const DialogButtonHolder = styled.div`
	margin-top: 30px;
	display: flex;
	justify-content: flex-end;
`;
