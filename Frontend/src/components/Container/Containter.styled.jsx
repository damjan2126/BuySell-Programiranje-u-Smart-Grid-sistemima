import styled from "styled-components";

export const Container = styled.main`
	max-width: ${(props) => (props.width ? props.width : "1020px")};
	margin: 0 auto;
	padding: 10px;
`;
