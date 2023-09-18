import { AiOutlineDown } from "react-icons/ai";
import Select, { components } from "react-select";
import { options, selectStyles } from "./HeaderSelect.constants";
import { useDispatch } from "react-redux";
import { removeUser } from "../../../store/features/userSlice/userSlice";
import UserIcon from "./UserIcon";
import { useNavigate } from "react-router-dom";
import { useQueryClient } from "react-query";

const HeaderSelect = () => {
   const dispatch = useDispatch();
   const navigate = useNavigate();
   const logOut = () => dispatch(removeUser());
   const query = useQueryClient();
   const enterFullscreen = () => document.documentElement.requestFullscreen();

   const handleOnChange = (options) => {
      if (options.value === "logout") {
         query.clear();
         logOut();
      } else if (options.value === "fullscreen") enterFullscreen();
      else navigate(options.value);
   };

   return (
      <Select
         components={{
            DropdownIndicator: () => <AiOutlineDown />,
            Control: ({ children, ...props }) => (
               <components.Control {...props}>
                  <UserIcon />
                  {children}
               </components.Control>
            ),
         }}
         value={{ value: "user", label: "" }}
         isSearchable={false}
         onChange={handleOnChange}
         options={options}
         styles={selectStyles}
         maxMenuHeight={200}
         isMulti={false}
      />
   );
};

export default HeaderSelect;
