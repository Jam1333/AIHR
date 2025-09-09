import { MicrophoneButton } from "../UI/MicrophoneButton"

export const Chat = () => {
    return (
        <>
            <div className="w-[100vw] min-h-[80vh] flex flex-row items-center justify-center">
                <div className="w-[60vw] h-[60vh] flex flex-col rounded-3xl">

                    {/* Header */}
                    <div className="bg-neutral-900 shadow-sm rounded-t-3xl">
                        <div className="max-w-3xl mx-auto py-3 px-4 flex items-center justify-between">
                            <div className="flex items-center space-x-4">
                            <div>
                                <h1 className="text-xl font-semibold text-emerald-300">AIHR Chat</h1>
                            </div>
                            </div>
                        </div>
                    </div>

                    <div className="flex-1 overflow-y-auto p-4 space-y-6 bg-neutral-800">
                        <div className="flex items-start space-x-2">
                            <div className="flex flex-col">
                                <div className="bg-neutral-950 p-3 rounded-xl rounded-tl-none shadow-sm max-w-xs lg:max-w-md">
                                    <div className="flex flex-row items-center gap-5 text-sm text-white">Hi there! How can I help you today? <button className="bg-blue-500 w-6 h-6 rounded-full transition duration-150 hover:scale-110"></button></div>
                                </div>
                                <span className="text-xs text-gray-500 mt-1">10:23 AM</span>
                            </div>
                        </div>
                
                        <div className="flex items-start justify-end space-x-2">
                            <div className="flex flex-col items-end">
                                <div className="bg-emerald-500 p-3 rounded-xl rounded-tr-none shadow-sm max-w-xs lg:max-w-md">
                                    <p className="text-sm text-white">Hello! I have a question about your services.</p>
                                </div>
                                <span className="text-xs text-gray-500 mt-1">10:24 AM</span>
                            </div>
                        </div>
                    </div>
                    <div className="bg-neutral-800 px-4 py-4 mb-2 sm:mb-0 rounded-b-3xl">
                        <div className="relative flex">
                            {/* Я понимаю, что тут этот инпут нахуй не нужон) */}
                            <input type="text" placeholder="Write your message!" className="w-full focus:outline-none focus:placeholder-gray-500 text-gray-100 placeholder-gray-200 pl-12 bg-neutral-900 rounded-xl py-3"/>

                            <div className="absolute right-0 items-center inset-y-0 hidden sm:flex">
                                {/* <MicrophoneButton onSpeechEnded={} /> */}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}