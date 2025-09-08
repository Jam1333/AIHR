import { ErrorComponent } from "../UI/ErrorComponent";
import { HeaderComponent } from "../UI/HeaderComponent";
import { InterviewsList } from "../UI/InterviewsList";
import { NavLinkComponent } from "../UI/NavLinkComponent";
import { Registration } from "./Registration";
import { Vacancy } from "./Vacancy";

export const UI = () => {
  return (
    <>
      <div className="flex max-h-[90vh] items-center justify-center">
        {/* <HeaderComponent /> */}
        {/* <ErrorComponent status={400} message={"Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ"}/> */}
        {/* <InterviewList /> */}
        <Vacancy />
      </div>
    </>
  );
};
