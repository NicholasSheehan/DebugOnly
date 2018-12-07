#! /bin/sh

UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

echo "Unity path: ${UNITY_PATH}"

activateLicense() {
    echo "Activate Unity"

    ${UNITY_PATH} \
        -serial ${UNITY_SERIAL} \
        -username ${UNITY_USER} \
        -password ${UNITY_PWD} \
        -batchmode \
        -noUpm \
        -quit
}

buildPackage() {
	echo "Project Path: ${TRAVIS_BUILD_DIR}"
	ls "${TRAVIS_BUILD_DIR}"
	
	${UNITY_PATH} \
         -batchmode \
         -silent-crashes \
		 -stackTraceLogType "Script Only" \
         -logFile "${TRAVIS_BUILD_DIR}/unity.build.package.log" \
         -projectPath "${TRAVIS_BUILD_DIR}" \
         -executeMethod DebugOnlyPackageBuilder.Build \
         -quit

     rc1=$?
     echo "Build logs (Package)"
     cat "${TRAVIS_BUILD_DIR}/unity.build.package.log"

     # exit if build failed
     if [ $rc1 -ne 0 ]; then { echo "Build failed"; exit $rc1; } fi
}

returnLicense() {
    echo "Return license"

    ${UNITY_PATH} \
        -batchmode \
        -returnlicense \
        -quit
}

activateLicense
buildPackage
returnLicense

echo "${TRAVIS_BUILD_DIR} Contents:"
ls "${TRAVIS_BUILD_DIR}"

exit 0