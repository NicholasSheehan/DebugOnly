#! /bin/sh

UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

activateLicense() {
	echo ""
	echo ""
    echo "Activate Unity"
	echo ""
	echo ""
	
    ${UNITY_PATH} \
        -serial ${UNITY_SERIAL} \
        -username ${UNITY_USER} \
        -password ${UNITY_PWD} \
        -batchmode \
        -noUpm \
        -quit
}

buildPackage() {
	echo ""
	echo ""
	echo "Project Path: ${TRAVIS_BUILD_DIR}"
	echo ""
	echo ""
	
	${UNITY_PATH} \
         -batchmode \
         -silent-crashes \
		 -stackTraceLogType "Script Only" \
         -logFile "${TRAVIS_BUILD_DIR}/unity.build.package.log" \
         -projectPath "${TRAVIS_BUILD_DIR}" \
         -executeMethod DebugOnlyPackageBuilder.Build \
         -quit

    rc1=$?
	 
	echo ""
	echo ""
    echo "Build logs (Package)"
    cat "${TRAVIS_BUILD_DIR}/unity.build.package.log"
    # exit if build failed
    if [ $rc1 -ne 0 ]; then { echo "Build failed"; exit $rc1; } fi
}

returnLicense() {
	echo ""
	echo ""
    echo "Return license"
	echo ""
	echo ""
	
    ${UNITY_PATH} \
        -batchmode \
        -returnlicense \
        -quit
}

activateLicense
buildPackage
returnLicense

exit 0