interface NavLinkProps {
    path?: string;
    linkName?: string;
}

export const NavLinkComponent = ({path, linkName}: NavLinkProps) => {
    return(
        <>
            <a href={path} className="w-auto h-auto rounded-xl bg-emerald-400 px-[1vw] py-[.5vh] text-white text-xl shadow-lg shadow-emerald-400/0 transition-all duration-300 hover:shadow-emerald-400/100">{linkName}</a>
        </>
    )
}