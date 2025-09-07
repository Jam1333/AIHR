import { ErrorComponent } from "../UI/ErrorComponent"
import { HeaderComponent } from "../UI/HeaderComponent"
import { NavLinkComponent } from "../UI/NavLinkComponent"

export const UI = () => {
    return (
        <>
            <div className="flex h-[90vh] items-center justify-center">
                {/* <HeaderComponent /> */}
                <ErrorComponent status={500} message={"Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ Старый БОГ"}/>
            </div>
        </>
    )
}