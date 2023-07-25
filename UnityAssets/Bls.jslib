var BLS = {
  VerifySignature: function (publicKey, messageHash, signature) {
    return blsVerify(publicKey, messageHash, signature);
  },
};
mergeInto(LibraryManager.library, BLS);
